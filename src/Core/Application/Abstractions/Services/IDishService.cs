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
