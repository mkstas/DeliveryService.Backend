using DeliveryService.Domain.Interfaces.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Web.Contracts.Permissions;
using DeliveryService.Web.Contracts.Roles;
using DeliveryService.Web.Contracts.Users;
using DeliveryService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserMeAsync()
        {
            var user = await userService.GetUserByIdAsync(User.GetCurrentUserId());

            List<RoleResponse> roles = [.. user.Roles.Select(role => new RoleResponse(
                role.Id, role.Name,
                [.. role.Permissions.Select(permission => new PermissionResponse(permission.Id, permission.DisplayName))]))];

            UserMeResponse response = new(user.Id, user.EmailAddress, user.Name,
                user.ImageUrl is null ? null : (string)user.ImageUrl,
                roles,
                [.. user.PaymentMethods.Select(pm => new PaymentMethodResponse(pm.Id, pm.CardNumber))]);

            return Ok(response);
        }

        [HttpPatch("me/email")]
        [Authorize]
        public async Task<IActionResult> UpdateUserMeEmailAsync([FromBody] UpdateUserEmailRequest request)
        {
            await userService.UpdateUserEmailAsync(User.GetCurrentUserId(), EmailAddress.Create(request.EmailAddress));

            return NoContent();
        }
    }
}
