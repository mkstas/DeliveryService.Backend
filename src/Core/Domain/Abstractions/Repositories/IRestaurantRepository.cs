
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to restaurants.
    /// </summary>
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="restaurant">The restaurant to create.</param>
        Task CreateRestaurantAsync(Restaurant restaurant);

        /// <summary>
        /// Returns an restaurant by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <returns>The matching restaurant, or <c>null</c> if none found.</returns>
        Task<Restaurant?> GetRestaurantByIdAsync(Guid restaurantId);

        /// <summary>
        /// Returns restaurants in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the restaurant name.</param>
        /// <returns>The matching restaurants.</returns>
        Task<List<Restaurant>> GetRestaurantsAsync(string? search = null);

        /// <summary>
        /// Returns the restaurant the specified user is assigned to, or <c>null</c> if none.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user restaurant, or <c>null</c> if none found.</returns>
        Task<Restaurant?> GetUserRestaurantAsync(Guid userId);

        /// <summary>
        /// Updates the specified restaurant.
        /// </summary>
        /// <param name="restaurant">The restaurant to update.</param>
        Task UpdateRestaurantAsync(Restaurant restaurant);
    }
}
