using Application.Abstractions.Services;
using Application.DTOs.Roles;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Roles.Create)]
        public async Task<ActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            await roleService.CreateRoleAsync(request);

            return Created();
        }

        [HttpGet]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<ActionResult<IReadOnlyList<RoleResponse>>> GetRolesAsync()
        {
            var roles = await roleService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpPut("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<ActionResult> UpdateRoleAsync(
            [FromRoute] Guid roleId,
            [FromBody] UpdateRoleRequest request)
        {
            await roleService.UpdateRoleAsync(roleId, request);

            return Ok();
        }

        [HttpDelete("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Delete)]
        public async Task<ActionResult> RemoveRoleAsync([FromRoute] Guid roleId)
        {
            await roleService.RemoveRoleAsync(roleId);

            return NoContent();
        }

        [HttpPost("permissions")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<IActionResult> AddRolePermissionAsync([FromBody] AddRolePermissionsRequest request)
        {
            await roleService.AddRolePermissionAsync(request.RoleId, request.PermissionId);

            return NoContent();
        }

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

