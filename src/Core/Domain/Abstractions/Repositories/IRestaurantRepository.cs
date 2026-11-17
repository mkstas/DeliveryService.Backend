
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
        /// Returns all restaurants in the system.
        /// </summary>
        /// <returns>All restaurants in the system.</returns>
        Task<List<Restaurant>> GetRestaurantsAsync();

        /// <summary>
        /// Updates the specified restaurant.
        /// </summary>
        /// <param name="restaurant">The restaurant to update.</param>
        Task UpdateRestaurantAsync(Restaurant restaurant);
    }
}
