using Microsoft.AspNetCore.Authorization;

namespace DeliveryService.Infrastructure.Authorization
{
    public class PermissionRequirement(string[] permissionSystemNames) : IAuthorizationRequirement
    {
        public string[] PermissionSystemNames { get; } = permissionSystemNames ?? [];
    }
}
