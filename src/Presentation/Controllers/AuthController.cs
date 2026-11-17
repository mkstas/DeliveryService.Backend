using Application.Abstractions.Services;
using Application.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for user authentication and email confirmation.
    /// </summary>
    /// <param name="userService">The user service.</param>
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The request to register user.</param>
        /// <returns>An <see cref="ActionResult"/> representing a created response.</returns>
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] CreateUserRequest request)
        {
            await userService.CreateUserAsync(request);

            return Created();
        }

        /// <summary>
        /// Authenticates a user and sets the access token cookie.
        /// </summary>
        /// <param name="request">The request to log in user.</param>
        /// <returns>An <see cref="ActionResult"/> representing a successful login.</returns>
        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginUserRequest request)
        {
            string accessToken = await userService.LoginUserAsync(request);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };

            HttpContext.Response.Cookies.Append("access_token", accessToken, cookieOptions);

            return Ok();
        }

        /// <summary>
        /// Logs out the current user by deleting the access token cookie.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> representing a no-content response.</returns>
        [HttpDelete("logout")]
        [Authorize]
        public ActionResult LogoutUser()
        {
            HttpContext.Response.Cookies.Delete("access_token");

            return NoContent();
        }

        /// <summary>
        /// Confirms the email address of a user.
        /// </summary>
        /// <param name="request">The request to confirm email.</param>
        /// <returns>An <see cref="IActionResult"/> representing a created response.</returns>
        [HttpPost("email/confirm")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request)
        {
            await userService.ConfirmUserEmailAsync(request);

            return Created();
        }

        /// <summary>
        /// Resends the email confirmation message.
        /// </summary>
        /// <param name="request">The request to resend confirmation.</param>
        /// <returns>An <see cref="IActionResult"/> representing a successful response.</returns>
        [HttpPost("email/resend")]
        public async Task<IActionResult> ResendConfirmationAsync([FromBody] ResendConfirmEmailRequest request)
        {
            await userService.ResendConfirmUserEmailAsync(request);

            return Ok();
        }
    }
}
