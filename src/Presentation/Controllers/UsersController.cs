using Application.Abstractions.Services;
using Application.DTOs.Users;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [HasPermission(Permissions.Users.Read)]
        public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetUsersAsync()
        {
            var users = await userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpPost]
        [HasPermission(Permissions.Users.Create)]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            await userService.CreateUserByAdminAsync(request);

            return Created();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetMeAsync()
        {
            var user = await userService.GetUserByIdAsync(User.GetCurrentId());

            return Ok(user);
        }

        [HttpPatch("me")]
        [Authorize]
        public async Task<ActionResult> UpdateMeAsync([FromBody] UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(User.GetCurrentId(), request);

            return Ok();
        }

        [HttpPost("roles")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] AddUserRoleRequest request)
        {
            await userService.AddUserRoleAsync(request.UserId, request.RoleId);

            return Created();
        }

        [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<IActionResult> RemoveUserRoleAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid roleId)
        {
            await userService.RemoveUserRoleAsync(userId, roleId);

            return NoContent();
        }

        [HttpPost("payments")]
        [Authorize]
        public async Task<IActionResult> AddPaymentAsync([FromBody] AddPaymentRequest request)
        {
            await userService.AddPaymentAsync(User.GetCurrentId(), request);

            return Created();
        }

        [HttpDelete("payments/{paymentId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemovePaymentMethodAsync([FromRoute] Guid paymentId)
        {
            await userService.RemovePaymentAsync(User.GetCurrentId(), paymentId);

            return NoContent();
        }
    }
}

