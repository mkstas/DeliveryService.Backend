using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to dish specifications.
    /// </summary>
    public interface IDishSpecificationRepository
    {
        /// <summary>
        /// Creates a new dish specification.
        /// </summary>
        /// <param name="dishSpecification">The dish specification to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateDishSpecificationAsync(DishSpecification dishSpecification);

        /// <summary>
        /// Returns a dish specification by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="dishSpecificationId">The dish specification identifier.</param>
        /// <returns>The matching dish specification, or <c>null</c> if none found.</returns>
        Task<DishSpecification?> GetDishSpecificationByIdAsync(Guid dishSpecificationId);

        /// <summary>
        /// Returns all dish specifications in the system.
        /// </summary>
        /// <returns>All dish specifications in the system.</returns>
        Task<List<DishSpecification>> GetDishSpecificationsAsync();

        /// <summary>
        /// Updates the specified dish specification.
        /// </summary>
        /// <param name="dishSpecification">The dish specification to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateDishSpecificationAsync(DishSpecification dishSpecification);

        /// <summary>
        /// Removes the specified dish specification.
        /// </summary>
        /// <param name="dishSpecification">The dish specification to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveDishSpecificationAsync(DishSpecification dishSpecification);
    }
}
