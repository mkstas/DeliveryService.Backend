using Application.Abstractions.Services;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user, restaurant and dish images.
    /// </summary>
    /// <param name="userService">The user service.</param>
    /// <param name="restaurantService">The restaurant service.</param>
    /// <param name="dishService">The dish service.</param>
    [ApiController]
    [Route("[controller]")]
    public class StaticController(
        IUserService userService,
        IRestaurantService restaurantService,
        IDishService dishService) : ControllerBase
    {
        private const long MaxImageSize = 5 * 1024 * 1024;

        private static readonly Dictionary<string, string> AllowedImageTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            ["image/jpeg"] = ".jpg",
            ["image/png"] = ".png",
            ["image/webp"] = ".webp",
            ["image/gif"] = ".gif",
        };

        /// <summary>
        /// Uploads a profile image for the current user.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("users")]
        [Authorize]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadCurrentUserImageAsync([FromForm] IFormFile? file)
        {
            var extension = GetImageExtension(file);

            await using var stream = file!.OpenReadStream();
            await userService.UploadUserImageAsync(User.GetCurrentId(), stream, file.ContentType, extension);

            return Created();
        }

        /// <summary>
        /// Retrieves a user image.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains the image file stream and its content type.</returns>
        [HttpGet("users/{imageName}")]
        [Authorize]
        public async Task<IActionResult> GetUserImageAsync([FromRoute] string imageName)
        {
            var (stream, contentType) = await userService.GetUserImageAsync(imageName);

            return File(stream, contentType);
        }

        /// <summary>
        /// Removes the profile image for the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("users")]
        [Authorize]
        public async Task<IActionResult> RemoveCurrentUserImageAsync()
        {
            await userService.RemoveUserImageAsync(User.GetCurrentId());

            return NoContent();
        }

        /// <summary>
        /// Uploads an image for the restaurant assigned to the current user.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("restaurants/me/image")]
        [HasPermission(Permissions.Restaurants.Update)]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadMyRestaurantImageAsync([FromForm] IFormFile? file)
        {
            var extension = GetImageExtension(file);

            var restaurant = await restaurantService.GetUserRestaurantAsync(User.GetCurrentId());

            await using var stream = file!.OpenReadStream();
            await restaurantService.UploadRestaurantImageAsync(restaurant.Id, stream, file.ContentType, extension);

            return Created();
        }

        /// <summary>
        /// Removes the image of the restaurant assigned to the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("restaurants/me/image")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<IActionResult> RemoveMyRestaurantImageAsync()
        {
            var restaurant = await restaurantService.GetUserRestaurantAsync(User.GetCurrentId());

            await restaurantService.RemoveRestaurantImageAsync(restaurant.Id);

            return NoContent();
        }

        /// <summary>
        /// Uploads an image for a dish of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("dishes/me/{dishId:guid}/image")]
        [HasPermission(Permissions.Dishes.Update)]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadMyDishImageAsync([FromRoute] Guid dishId, [FromForm] IFormFile? file)
        {
            var extension = GetImageExtension(file);

            await dishService.EnsureUserOwnsDishAsync(User.GetCurrentId(), dishId);

            await using var stream = file!.OpenReadStream();
            await dishService.UploadDishImageAsync(dishId, stream, file.ContentType, extension);

            return Created();
        }

        /// <summary>
        /// Removes the image of a dish of the restaurant assigned to the current user.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("dishes/me/{dishId:guid}/image")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> RemoveMyDishImageAsync([FromRoute] Guid dishId)
        {
            await dishService.EnsureUserOwnsDishAsync(User.GetCurrentId(), dishId);

            await dishService.RemoveDishImageAsync(dishId);

            return NoContent();
        }

        /// <summary>
        /// Uploads an image for the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("restaurants/{restaurantId:guid}/image")]
        [HasPermission(Permissions.Restaurants.Update)]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadRestaurantImageAsync([FromRoute] Guid restaurantId, [FromForm] IFormFile? file)
        {
            var extension = GetImageExtension(file);

            await using var stream = file!.OpenReadStream();
            await restaurantService.UploadRestaurantImageAsync(restaurantId, stream, file.ContentType, extension);

            return Created();
        }

        /// <summary>
        /// Retrieves a restaurant image.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains the image file stream and its content type.</returns>
        [HttpGet("restaurants/{imageName}")]
        [HasPermission(Permissions.Restaurants.Read)]
        public async Task<IActionResult> GetRestaurantImageAsync([FromRoute] string imageName)
        {
            var (stream, contentType) = await restaurantService.GetRestaurantImageAsync(imageName);

            return File(stream, contentType);
        }

        /// <summary>
        /// Removes the image of the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The restaurant identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("restaurants/{restaurantId:guid}/image")]
        [HasPermission(Permissions.Restaurants.Update)]
        public async Task<IActionResult> RemoveRestaurantImageAsync([FromRoute] Guid restaurantId)
        {
            await restaurantService.RemoveRestaurantImageAsync(restaurantId);

            return NoContent();
        }

        /// <summary>
        /// Uploads an image for the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("dishes/{dishId:guid}/image")]
        [HasPermission(Permissions.Dishes.Update)]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadDishImageAsync([FromRoute] Guid dishId, [FromForm] IFormFile? file)
        {
            var extension = GetImageExtension(file);

            await using var stream = file!.OpenReadStream();
            await dishService.UploadDishImageAsync(dishId, stream, file.ContentType, extension);

            return Created();
        }

        /// <summary>
        /// Retrieves a dish image.
        /// </summary>
        /// <param name="imageName">The name of the image in the storage.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains the image file stream and its content type.</returns>
        [HttpGet("dishes/{imageName}")]
        [HasPermission(Permissions.Dishes.Read)]
        public async Task<IActionResult> GetDishImageAsync([FromRoute] string imageName)
        {
            var (stream, contentType) = await dishService.GetDishImageAsync(imageName);

            return File(stream, contentType);
        }

        /// <summary>
        /// Removes the image of the specified dish.
        /// </summary>
        /// <param name="dishId">The dish identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("dishes/{dishId:guid}/image")]
        [HasPermission(Permissions.Dishes.Update)]
        public async Task<IActionResult> RemoveDishImageAsync([FromRoute] Guid dishId)
        {
            await dishService.RemoveDishImageAsync(dishId);

            return NoContent();
        }

        private static string GetImageExtension(IFormFile? file)
        {
            if (file is null || file.Length == 0)
                throw new ArgumentException("Image file is required.");

            if (file.Length > MaxImageSize)
                throw new ArgumentException($"Image size cannot exceed {MaxImageSize / 1024 / 1024} MB.");

            if (!AllowedImageTypes.TryGetValue(file.ContentType, out var extension))
                throw new ArgumentException("Unsupported image content type.");

            return extension;
        }
    }
}
