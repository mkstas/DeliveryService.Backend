using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Web.Contracts.Users;
using DeliveryService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("static/users")]
    public class StaticUsersController(IUserService userService) : ControllerBase
    {
        private const long MaxImageSize = 5 * 1024 * 1024;

        private static readonly Dictionary<string, string> AllowedImageTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            ["image/jpeg"] = ".jpg",
            ["image/png"] = ".png",
            ["image/webp"] = ".webp",
            ["image/gif"] = ".gif",
        };

        [HttpGet("image/{imageName}")]
        [Authorize]
        public async Task<IActionResult> GetUserImageAsync([FromRoute] string imageName)
        {
            var (stream, contentType) = await userService.GetUserImageAsync(imageName);

            return File(stream, contentType);
        }

        [HttpPost("image")]
        [Authorize]
        [RequestSizeLimit(MaxImageSize)]
        public async Task<IActionResult> UploadCurrentUserImageAsync([FromForm] IFormFile? file)
        {
            if (file is null || file.Length == 0)
            {
                throw new ArgumentException("Image file is required.");
            }

            if (file.Length > MaxImageSize)
            {
                throw new ArgumentException($"Image size cannot exceed {MaxImageSize / 1024 / 1024} MB.");
            }

            if (!AllowedImageTypes.TryGetValue(file.ContentType, out var extension))
            {
                throw new ArgumentException("Unsupported image content type.");
            }

            await using var stream = file.OpenReadStream();
            await userService.UploadUserImageAsync(User.GetCurrentUserId(), stream, file.ContentType, extension);

            return NoContent();
        }

        [HttpDelete("image")]
        [Authorize]
        public async Task<IActionResult> RemoveCurrentUserImageAsync()
        {
            await userService.RemoveUserImageAsync(User.GetCurrentUserId());

            return NoContent();
        }
    }
}
