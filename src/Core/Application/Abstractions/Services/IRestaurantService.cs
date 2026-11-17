using Application.DTOs.Restaurants;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing restaurants.
    /// </summary>
    public interface IRestaurantService
    {
        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The request to create restaurant.</param>
        Task CreateRestaurantAsync(CreateRestaurantRequest request);

        /// <summary>
        /// Returns a restaurant by its identifier.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <returns>The matching restaurant.</returns>
        Task<RestaurantResponse> GetRestaurantByIdAsync(Guid restaurantId);

        /// <summary>
        /// Returns all restaurants in the system.
        /// </summary>
        /// <returns>All restaurants in the system.</returns>
        Task<IReadOnlyList<RestaurantResponse>> GetRestaurantsAsync();

        /// <summary>
        /// Updates the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The request to update restaurant.</param>
        Task UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantRequest request);

        /// <summary>
        /// Assigns a user to the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The request to add user to restaurant.</param>
        Task AddRestaurantUserAsync(Guid restaurantId, AddRestaurantUserRequest request);

        /// <summary>
        /// Removes the user assignment from the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="userId">The user identifier.</param>
        Task RemoveRestaurantUserAsync(Guid restaurantId, Guid userId);

        /// <summary>
        /// Adds an address to the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The request to add address.</param>
        Task AddRestaurantAddressAsync(Guid restaurantId, AddRestaurantAddressRequest request);

        /// <summary>
        /// Removes an address from the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="addressId">The restaurant address identifier.</param>
        Task RemoveRestaurantAddressAsync(Guid restaurantId, Guid addressId);
    }
}
