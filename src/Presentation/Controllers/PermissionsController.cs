using Application.Abstractions.Services;
using Application.DTOs.Permissions;
using Domain.Abstractions.Permissions;
using Infrastructure.AuthService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for retrieving permissions.
    /// </summary>
    /// <param name="permissionService">The permission service.</param>
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController(IPermissionService permissionService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all permissions in the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with all permissions.</returns>
        [HttpGet]
        [HasPermission(Permissions.Self.Read)]
        public async Task<ActionResult<IReadOnlyList<PermissionResponse>>> GetPermissionsAsync()
        {
            var response = await permissionService.GetPermissionsAsync();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves the system names of the permissions granted to the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains a <c>200 OK</c> response with the current user permission names.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMyPermissionsAsync()
        {
            var response = await permissionService.GetUserPermissionNamesAsync(User.GetCurrentId());

            return Ok(response);
        }
    }
}

