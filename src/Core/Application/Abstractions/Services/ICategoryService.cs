using Application.DTOs.Categories;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        Task CreateCategoryAsync(CreateCategoryRequest request);

        /// <summary>
        /// Returns all categories in the system.
        /// </summary>
        /// <returns>All categories in the system.</returns>
        Task<IReadOnlyList<CategoryResponse>> GetCategoriesAsync();

        /// <summary>
        /// Returns a category by its identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The matching category.</returns>
        Task<CategoryResponse> GetCategoryByIdAsync(Guid categoryId);

        /// <summary>
        /// Updates the name of the specified category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="request">The request to update category.</param>
        Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryRequest request);

        /// <summary>
        /// Remove the specified category from the system.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        Task RemoveCategoryAsync(Guid categoryId);
    }
}
