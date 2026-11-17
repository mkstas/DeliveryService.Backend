using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to categories.
    /// </summary>
    public interface ICategoryRespository
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">The category to create.</param>
        Task CreateCategoryAsync(Category category);

        /// <summary>
        /// Returns a category by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The matching category, or <c>null</c> if none found.</returns>
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);

        /// <summary>
        /// Returns all categories in the system.
        /// </summary>
        /// <returns>All categories in the system.</returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="category">The category to update.</param>
        Task UpdateCategoryAsync(Category category);

        /// <summary>
        /// Removes the specified category.
        /// </summary>
        /// <param name="category">The category to remove.</param>
        Task RemoveCategoryAsync(Category category);
    }
}
