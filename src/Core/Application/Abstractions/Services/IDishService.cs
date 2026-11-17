using Application.DTOs.Dishes;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing dishes.
    /// </summary>
    public interface IDishService
    {
        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="request">The request to create dish.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateDishAsync(CreateDishRequest request);

        /// <summary>
        /// Returns all dishes in the system.
        /// </summary>
        /// <returns>All dishes in the system.</returns>
        Task<IReadOnlyList<DishResponse>> GetDishesAsync();

        /// <summary>
        /// Returns a dish by its identifier.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>The matching dish.</returns>
        Task<DishResponse> GetDishByIdAsync(Guid dishId);

        /// <summary>
        /// Updates the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to update dish.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateDishAsync(Guid dishId, UpdateDishRequest request);

        /// <summary>
        /// Remove the specified dish from the system.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveDishAsync(Guid dishId);
    }
}
