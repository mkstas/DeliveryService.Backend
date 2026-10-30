using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Domain.ValueObjects;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Permissions;
using DeliveryService.Web.Contracts.Roles;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpPost]
        [HasPermission(Permissions.Roles.Create)]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            await roleService.CreateRoleAsync(StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpGet]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await roleService.GetRolesAsync();

            List<RoleResponse> response = [.. roles.Select(r => new RoleResponse(
                r.Id, r.Name,
                [.. r.Permissions.Select(permission => new PermissionResponse(permission.Id, permission.DisplayName))]))];

            return Ok(response);
        }

        [HttpGet("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Read)]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid roleId)
        {
            var role = await roleService.GetRoleByIdAsync(roleId);

            var response = new RoleResponse(role.Id, role.Name,
                [.. role.Permissions.Select(permission => new PermissionResponse(permission.Id, permission.DisplayName))]);

            return Ok(response);
        }

        [HttpPut("{roleId:guid}")]
        [HasPermission(Permissions.Roles.Update)]
        public async Task<IActionResult> UpdateRoleAsync(
            [FromRoute] Guid roleId,
            [FromBody] UpdateRoleRequest request)
        {
            await roleService.UpdateRoleAsync(roleId, StringBounded.Create(request.Name));

            return NoContent();
        }

        [HttpPost("permissions")]
        [HasPermission(Permissions.Access.ManageRolePermissions)]
        public async Task<IActionResult> AddRolePermissionAsync([FromBody] AddRolePermissionsRequest request)
        {
            await roleService.AddRolePermissionAsync(request.RoleId, request.PermissionId);

            return NoContent();
        }

        [HttpDelete("{roleId:guid}/permissions/{permissionId:guid}")]
        [HasPermission(Permissions.Access.ManageRolePermissions)]
        public async Task<IActionResult> RemoveRolePermissionAsync(
            [FromRoute] Guid roleId,
            [FromRoute] Guid permissionId)
        {
            await roleService.RemoveRolePermissionAsync(roleId, permissionId);

            return NoContent();
        }
    }
}
