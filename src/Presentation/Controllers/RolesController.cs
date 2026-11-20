using Application.Abstractions.Services;
using Application.Common;
using Application.DTOs.Common;
using Application.DTOs.Roles;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for managing roles.
    /// </summary>
    /// <param name="roleService">The role service.</param>
    [ApiController]
    [Route("[controller]")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="request">The role creation request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>201 Created</c> response.</returns>
        [HttpPost]
        [HasPermission(Permissions.Roles.Create)]
        public async Task<ActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            await roleService.CreateRoleAsync(request);

            return Created();
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all roles.</returns>
        [HttpGet]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<IReadOnlyList<RoleResponse>>> GetRolesAsync()
        {
            var roles = await roleService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("page")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<PagedResponse<RoleResponse>>> GetRolesPageAsync([FromQuery] PaginationQuery pagination)
        {
            var roles = await roleService.GetRolesAsync();

            return Ok(roles.ToPaged(pagination));
        }

        /// <summary>
        /// Retrieves a role by its identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the matching role.</returns>
        [HttpGet("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<RoleResponse>> GetRoleByIdAsync([FromRoute] Guid roleId)
        {
            var role = await roleService.GetRoleByIdAsync(roleId);

            return Ok(role);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="request">The role update request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response.</returns>
        [HttpPut("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<ActionResult> UpdateRoleAsync(
            [FromRoute] Guid roleId,
            [FromBody] UpdateRoleRequest request)
        {
            await roleService.UpdateRoleAsync(roleId, request);

            return Ok();
        }

        /// <summary>
        /// Removes a role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Delete)]
        public async Task<ActionResult> RemoveRoleAsync([FromRoute] Guid roleId)
        {
            await roleService.RemoveRoleAsync(roleId);

            return NoContent();
        }

        /// <summary>
        /// Adds a permission to a role.
        /// </summary>
        /// <param name="request">The role permission assignment request.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpPost("permissions")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<IActionResult> AddRolePermissionAsync([FromBody] AddRolePermissionsRequest request)
        {
            await roleService.AddRolePermissionAsync(request.RoleId, request.PermissionId);

            return NoContent();
        }

        /// <summary>
        /// Removes a permission from a role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="permissionId">The permission identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>204 No Content</c> response.</returns>
        [HttpDelete("{roleId:guid}/permissions/{permissionId:guid}")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<IActionResult> RemoveRolePermissionAsync(
            [FromRoute] Guid roleId,
            [FromRoute] Guid permissionId)
        {
            await roleService.RemoveRolePermissionAsync(roleId, permissionId);

            return NoContent();
        }
    }
}

