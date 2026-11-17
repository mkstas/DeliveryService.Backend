using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to dishes.
    /// </summary>
    public interface IDishRepository
    {
        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="dish">The dish to create.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateDishAsync(Dish dish);

        /// <summary>
        /// Returns a dish by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>The matching dish, or <c>null</c> if none found.</returns>
        Task<Dish?> GetDishByIdAsync(Guid dishId);

        /// <summary>
        /// Returns all dishes in the system.
        /// </summary>
        /// <returns>All dishes in the system.</returns>
        Task<List<Dish>> GetDishesAsync();

        /// <summary>
        /// Updates the specified dish.
        /// </summary>
        /// <param name="dish">The dish to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateDishAsync(Dish dish);

        /// <summary>
        /// Removes the specified dish.
        /// </summary>
        /// <param name="dish">The dish to remove.</param>
        /// <returns>A task that represents the asynchronous remove operation.</returns>
        Task RemoveDishAsync(Dish dish);
    }
}
