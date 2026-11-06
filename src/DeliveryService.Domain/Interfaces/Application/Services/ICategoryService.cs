using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Application.Services
{
    /// <summary>
    /// Provides operations for managing categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="name">The name of the category to create.</param>
        Task CreateCategoryAsync(StringBounded name);

        /// <summary>
        /// Returns a category by its identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>The matching category.</returns>
        Task<Category> GetCategoryByIdAsync(Guid categoryId);

        /// <summary>
        /// Returns all categories in the system.
        /// </summary>
        /// <returns>All categories in the system.</returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Updates the name of the specified category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="newName">The new name of the category.</param>
        Task UpdateCategoryAsync(Guid categoryId, StringBounded newName);
    }
}
