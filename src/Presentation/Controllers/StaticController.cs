using Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user images.
    /// </summary>
    /// <param name="userService">The user service.</param>
    [ApiController]
    [Route("[controller]")]
    public class StaticController(IUserService userService) : ControllerBase
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
            if (file is null || file.Length == 0)
                throw new ArgumentException("Image file is required.");

            if (file.Length > MaxImageSize)
                throw new ArgumentException($"Image size cannot exceed {MaxImageSize / 1024 / 1024} MB.");

            if (!AllowedImageTypes.TryGetValue(file.ContentType, out var extension))
                throw new ArgumentException("Unsupported image content type.");

            await using var stream = file.OpenReadStream();
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
    }
}
