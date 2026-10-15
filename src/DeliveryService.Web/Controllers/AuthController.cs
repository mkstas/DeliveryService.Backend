using DeliveryService.Domain.Interfaces.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Web.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthorizeUserRequest request)
        {
            var accessToken = await userService.LoginUserAsync(EmailAddress.Create(request.EmailAddress), request.Password);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };

            HttpContext.Response.Cookies.Append("access_token", accessToken, cookieOptions);

            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthorizeUserRequest request)
        {
            await userService.CreateUserAsync(
                EmailAddress.Create(request.EmailAddress), request.Password, StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpDelete("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("access_token");

            return NoContent();
        }
    }
}
