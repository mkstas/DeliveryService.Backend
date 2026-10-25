using DeliveryService.Domain.Common;
using DeliveryService.Domain.Interfaces.Application.Services;
using DeliveryService.Infrastructure.Authorization;
using DeliveryService.Web.Contracts.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController(IPermissionService permissionService) : ControllerBase
    {
        [HttpGet]
        [HasPermission(Permissions.Self.Read)]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            var permissons = await permissionService.GetPermissionsAsync();

            List<PermissionResponse> response = [.. permissons.Select(p => new PermissionResponse(p.Id, p.DisplayName))];

            return Ok(response);
        }
    }
}
