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
        /// Returns restaurants in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the restaurant name.</param>
        /// <returns>The matching restaurants.</returns>
        Task<IReadOnlyList<RestaurantResponse>> GetRestaurantsAsync(string? search = null);

        /// <summary>
        /// Returns the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user restaurant.</returns>
        Task<RestaurantResponse> GetUserRestaurantAsync(Guid userId);

        /// <summary>
        /// Updates the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The request to update restaurant.</param>
        Task UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantRequest request);

        /// <summary>
        /// Updates the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to update restaurant.</param>
        Task UpdateUserRestaurantAsync(Guid userId, UpdateRestaurantRequest request);

        /// <summary>
        /// Adds an address to the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to add address.</param>
        Task AddUserRestaurantAddressAsync(Guid userId, UpdateRestaurantAddressRequest request);

        /// <summary>
        /// Updates an address of the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="request">The request to update address.</param>
        Task UpdateUserRestaurantAddressAsync(Guid userId, Guid addressId, UpdateRestaurantAddressRequest request);

        /// <summary>
        /// Removes an address from the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        Task RemoveUserRestaurantAddressAsync(Guid userId, Guid addressId);

        /// <summary>
        /// Adds an ingredient to the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to add ingredient.</param>
        Task AddUserRestaurantIngredientAsync(Guid userId, AddMyRestaurantIngredientRequest request);

        /// <summary>
        /// Updates an ingredient of the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <param name="request">The request to update ingredient.</param>
        Task UpdateUserRestaurantIngredientAsync(Guid userId, Guid ingredientId, UpdateRestaurantIngredientRequest request);

        /// <summary>
        /// Removes an ingredient from the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        Task RemoveUserRestaurantIngredientAsync(Guid userId, Guid ingredientId);

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

        /// <summary>
        /// Updates an address of the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="addressId">The restaurant address identifier.</param>
        /// <param name="request">The request to update address.</param>
        Task UpdateRestaurantAddressAsync(Guid restaurantId, Guid addressId, UpdateRestaurantAddressRequest request);

        /// <summary>
        /// Adds an ingredient to the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="request">The request to add ingredient.</param>
        Task AddRestaurantIngredientAsync(Guid restaurantId, AddRestaurantIngredientRequest request);

        /// <summary>
        /// Updates the specified ingredient.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        /// <param name="request">The request to update ingredient.</param>
        Task UpdateRestaurantIngredientAsync(Guid restaurantId, Guid ingredientId, UpdateRestaurantIngredientRequest request);

        /// <summary>
        /// Removes an ingredient from the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="ingredientId">The ingredient identifier.</param>
        Task RemoveRestaurantIngredientAsync(Guid restaurantId, Guid ingredientId);

        /// <summary>
        /// Uploads and assigns an image to the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="stream">The image content stream.</param>
        /// <param name="contentType">The content type of the image.</param>
        /// <param name="extension">The file extension of the image.</param>
        /// <returns>The name of the uploaded image.</returns>
        Task<string> UploadRestaurantImageAsync(Guid restaurantId, Stream stream, string contentType, string extension);

        /// <summary>
        /// Returns a restaurant image by its name.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>The image content stream and its content type.</returns>
        Task<(Stream Stream, string ContentType)> GetRestaurantImageAsync(string imageName);

        /// <summary>
        /// Removes the image of the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        Task RemoveRestaurantImageAsync(Guid restaurantId);
    }
}
