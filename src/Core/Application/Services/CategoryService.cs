using Application.Abstractions.Cache;
using Application.Abstractions.Services;
using Application.DTOs.Categories;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Implementation of <see cref="ICategoryService"/>.
    /// </summary>
    internal class CategoryService(
        ICategoryRespository categoryRespository,
        ICacheService cacheService) : ICategoryService
    {
        /// <inheritdoc />
        public async Task CreateCategoryAsync(CreateCategoryRequest request)
        {
            await categoryRespository.CreateCategoryAsync(Category.Create(request.Name));
            await cacheService.RemoveAsync(CacheKeys.Categories.All);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<CategoryResponse>> GetCategoriesAsync(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var cachedResponse = await cacheService.GetAsync<IReadOnlyList<CategoryResponse>>(CacheKeys.Categories.All);

                if (cachedResponse is not null)
                    return cachedResponse;

                var categories = await categoryRespository.GetCategoriesAsync();

                IReadOnlyList<CategoryResponse> response = [.. categories.Select(c => new CategoryResponse(c.Id, c.Name))];

                await cacheService.SetAsync(CacheKeys.Categories.All, response, TimeSpan.FromDays(14));

                return response;
            }

            var filteredCategories = await categoryRespository.GetCategoriesAsync(search);

            return [.. filteredCategories.Select(c => new CategoryResponse(c.Id, c.Name))];
        }

        /// <inheritdoc />
        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await categoryRespository.GetCategoryByIdAsync(categoryId)
                ?? throw new NotFoundException<Category>(categoryId);

            var response = new CategoryResponse(category.Id, category.Name);

            return response;
        }

        /// <inheritdoc />
        public async Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryRequest request)
        {
            var category = await categoryRespository.GetCategoryByIdAsync(categoryId)
                ?? throw new NotFoundException<Category>(categoryId);

            category.ChangeName(request.Name);

            await categoryRespository.UpdateCategoryAsync(category);
            await cacheService.RemoveAsync(CacheKeys.Categories.All);
        }

        /// <inheritdoc />
        public async Task RemoveCategoryAsync(Guid categoryId)
        {
            var category = await categoryRespository.GetCategoryByIdAsync(categoryId)
                ?? throw new NotFoundException<Category>(categoryId);

            await categoryRespository.RemoveCategoryAsync(category);
            await cacheService.RemoveAsync(CacheKeys.Categories.All);
        }
    }
}
