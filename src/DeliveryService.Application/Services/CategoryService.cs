using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task CreateCategoryAsync(StringBounded name)
        {
            await categoryRepository.CreateCategoryAsync(Category.Create(name));
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await categoryRepository.GetCategoryByIdAsync(categoryId)
                ?? throw new NotFoundException<Category>(categoryId);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await categoryRepository.GetCategoriesAsync();
        }

        public async Task UpdateCategoryAsync(Guid categoryId, StringBounded newName)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(categoryId)
                ?? throw new NotFoundException<Category>(categoryId);

            category.ChangeName(newName);

            await categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
