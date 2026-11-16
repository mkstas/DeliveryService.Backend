using Application.Abstractions.Services;
using Application.DTOs.Permissions;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController(IPermissionService permissionService) : ControllerBase
    {
        [HttpGet]
        [HasPermission(Permissions.Self.Read)]
        public async Task<ActionResult<IReadOnlyList<PermissionResponse>>> GetPermissionsAsync()
        {
            var response = await permissionService.GetPermissionsAsync();

            return Ok(response);
        }
    }
}

