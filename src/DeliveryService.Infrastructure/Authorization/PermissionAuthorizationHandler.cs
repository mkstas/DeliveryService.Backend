using DeliveryService.Domain.Interfaces.Persistence.Postgres;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DeliveryService.Infrastructure.Authorization
{
    /// <summary>
    /// Handles <see cref="PermissionRequirement"/> by verifying the current user has all the required permissions.
    /// </summary>
    /// <param name="permissionRepository">The repository used to load the user's permissions.</param>
    public class PermissionAuthorizationHandler(IPermissionRepository permissionRepository)
        : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// Determines whether the current user satisfies the permission requirement and marks it as succeeded if so.
        /// </summary>
        /// <param name="context">The authorization handler context containing the current user.</param>
        /// <param name="requirement">The permission requirement to evaluate.</param>
        /// <returns>A task that represents the asynchronous evaluation.</returns>
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return;
            }

            if (requirement.PermissionSystemNames.Length == 0)
            {
                return;
            }

            var userPermissions = await permissionRepository.GetUserPermissionsByUserIdAsync(userId);

            var permissionSytemNames = userPermissions
                .Select(p => (string)p.SystemName)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (requirement.PermissionSystemNames.All(permissionSytemNames.Contains))
            {
                context.Succeed(requirement);
            }
        }
    }
}
