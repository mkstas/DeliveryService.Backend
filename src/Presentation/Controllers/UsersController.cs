using Application.Abstractions.Services;
using Application.DTOs.Users;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing users.
    /// </summary>
    /// <param name="userService">The user service.</param>
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all users.</returns>
        [HttpGet]
        [HasPermission(Permissions.Users.Read)]
        public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetUsersAsync()
        {
            var users = await userService.GetUsersAsync();

            return Ok(users);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The user creation request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Users.Create)]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            await userService.CreateUserByAdminAsync(request);

            return Created();
        }

        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the current user.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetMeAsync()
        {
            var user = await userService.GetUserByIdAsync(User.GetCurrentId());

            return Ok(user);
        }

        /// <summary>
        /// Updates the current user.
        /// </summary>
        /// <param name="request">The user update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPatch("me")]
        [Authorize]
        public async Task<ActionResult> UpdateMeAsync([FromBody] UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(User.GetCurrentId(), request);

            return Ok();
        }

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        /// <param name="request">The user role assignment request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("roles")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] AddUserRoleRequest request)
        {
            await userService.AddUserRoleAsync(request.UserId, request.RoleId);

            return Created();
        }

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<IActionResult> RemoveUserRoleAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid roleId)
        {
            await userService.RemoveUserRoleAsync(userId, roleId);

            return NoContent();
        }

        /// <summary>
        /// Adds a payment method for the current user.
        /// </summary>
        /// <param name="request">The payment addition request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost("payments")]
        [Authorize]
        public async Task<IActionResult> AddPaymentAsync([FromBody] AddPaymentRequest request)
        {
            await userService.AddPaymentAsync(User.GetCurrentId(), request);

            return Created();
        }

        /// <summary>
        /// Removes a payment method for the current user.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("payments/{paymentId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemovePaymentMethodAsync([FromRoute] Guid paymentId)
        {
            await userService.RemovePaymentAsync(User.GetCurrentId(), paymentId);

            return NoContent();
        }
    }
}

