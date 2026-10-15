using DeliveryService.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DeliveryService.Infrastructure.Authorization
{
    public class PermissionAuthorizationHandler(IPermissionRepository permissionRepository)
        : AuthorizationHandler<PermissionRequirement>
    {
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
