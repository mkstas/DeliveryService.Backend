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
        /// Returns dishes in the system, optionally filtered by name.
        /// </summary>
        /// <param name="search">Optional substring to match against the dish name.</param>
        /// <returns>The matching dishes.</returns>
        Task<IReadOnlyList<DishResponse>> GetDishesAsync(string? search = null);

        /// <summary>
        /// Returns dishes belonging to the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="search">Optional substring to match against the dish name.</param>
        /// <returns>The matching dishes.</returns>
        Task<IReadOnlyList<DishResponse>> GetDishesByRestaurantAsync(Guid restaurantId, string? search = null);

        /// <summary>
        /// Creates a dish in the restaurant assigned to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request to create dish.</param>
        /// <returns>The created dish.</returns>
        Task<DishResponse> CreateUserDishAsync(Guid userId, CreateDishRequest request);

        /// <summary>
        /// Verifies that the specified dish belongs to the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>The dish identifier when the dish belongs to the user restaurant.</returns>
        Task<Guid> EnsureUserOwnsDishAsync(Guid userId, Guid dishId);

        /// <summary>
        /// Adds an ingredient to the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to add the ingredient.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddDishIngredientAsync(Guid dishId, AddDishIngredientRequest request);

        /// <summary>
        /// Removes an ingredient from the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishIngredientId">The dish ingredient identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveDishIngredientAsync(Guid dishId, Guid dishIngredientId);

        /// <summary>
        /// Adds an ingredient to a dish of the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="request">The request to add the ingredient.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddUserDishIngredientAsync(Guid userId, Guid dishId, AddDishIngredientRequest request);

        /// <summary>
        /// Removes an ingredient from a dish of the restaurant assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="dishIngredientId">The dish ingredient identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveUserDishIngredientAsync(Guid userId, Guid dishId, Guid dishIngredientId);

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

        /// <summary>
        /// Uploads and assigns an image to the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="stream">The image content stream.</param>
        /// <param name="contentType">The content type of the image.</param>
        /// <param name="extension">The file extension of the image.</param>
        /// <returns>The name of the uploaded image.</returns>
        Task<string> UploadDishImageAsync(Guid dishId, Stream stream, string contentType, string extension);

        /// <summary>
        /// Returns a dish image by its name.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>The image content stream and its content type.</returns>
        Task<(Stream Stream, string ContentType)> GetDishImageAsync(string imageName);

        /// <summary>
        /// Removes the image of the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        Task RemoveDishImageAsync(Guid dishId);
    }
}
