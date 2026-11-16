using Application.Abstractions.Services;
using Application.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] CreateUserRequest request)
        {
            await userService.CreateUserAsync(request);

            return Created();
        }

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

        [HttpDelete("logout")]
        [Authorize]
        public ActionResult LogoutUser()
        {
            HttpContext.Response.Cookies.Delete("access_token");

            return NoContent();
        }

        [HttpPost("email/confirm")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request)
        {
            await userService.ConfirmUserEmailAsync(request);

            return Created();
        }

        [HttpPost("email/resend")]
        public async Task<IActionResult> ResendConfirmationAsync([FromBody] ResendConfirmEmailRequest request)
        {
            await userService.ResendConfirmUserEmailAsync(request);

            return Ok();
        }
    }
}
