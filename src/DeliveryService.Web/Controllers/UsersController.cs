using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
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

        [HttpPatch("me/bio")]
        [Authorize]
        public async Task<IActionResult> UpdateUserBioAsync([FromBody] UpdateUserBioRequest request)
        {
            await userService.UpdateUserBioAsync(User.GetCurrentUserId(), StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpPatch("me/email")]
        [Authorize]
        public async Task<IActionResult> UpdateUserMeEmailAsync([FromBody] UpdateUserEmailRequest request)
        {
            await userService.UpdateUserEmailAsync(User.GetCurrentUserId(), EmailAddress.Create(request.EmailAddress));

            return NoContent();
        }

        [HttpPost("payments")]
        [Authorize]
        public async Task<IActionResult> AddPaymentMethodAsync([FromBody] AddPaymentMethodRequest request)
        {
            await userService.AddPaymentMethodAsync(User.GetCurrentUserId(), CardNumber.Create(request.CardNumber));

            return NoContent();
        }

        [HttpDelete("payments/{paymentId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemovePaymentMethodAsync([FromRoute] Guid paymentId)
        {
            await userService.RemovePaymentMethodAsync(User.GetCurrentUserId(), paymentId);

            return NoContent();
        }

        [HttpPost("roles")]
        [HasPermission(Permissions.Access.ManageUserRoles)]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] AddUserRoleRequest request)
        {
            await userService.AddUserRoleAsync(request.UserId, request.RoleId);

            return NoContent();
        }

        [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
        [HasPermission(Permissions.Users.Delete)]
        public async Task<IActionResult> RemoveUserRoleAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid roleId)
        {
            await userService.RemoveUserRoleAsync(userId, roleId);

            return NoContent();
        }
    }
}
