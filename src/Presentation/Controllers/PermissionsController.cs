using Application.Abstractions.Services;
using Application.DTOs.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController(IPermissionService permissionService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PermissionResponse>>> GetPermissionsAsync()
        {
            var response = await permissionService.GetPermissionsAsync();

            return Ok(response);
        }
    }
}
