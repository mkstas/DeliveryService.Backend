using DeliveryService.Domain.Interfaces.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Web.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IUserService userService, IEmailVerificationService emailVerificationService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
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
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
        {
            await userService.CreateUserAsync(
                EmailAddress.Create(request.EmailAddress), request.Password, StringBounded.Create(request.Name));

            await emailVerificationService.SendConfirmationCodeAsync(EmailAddress.Create(request.EmailAddress));

            return NoContent();
        }

        [HttpDelete("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("access_token");

            return NoContent();
        }

        [HttpPost("email/verify")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request)
        {
            await emailVerificationService.ConfirmEmailAsync(EmailAddress.Create(request.EmailAddress), request.Code);

            return NoContent();
        }

        [HttpPost("email/resend")]
        public async Task<IActionResult> SendConfirmationAsync([FromBody] SendConfirmationRequest request)
        {
            await emailVerificationService.SendConfirmationCodeAsync(EmailAddress.Create(request.EmailAddress));

            return NoContent();
        }
    }
}
