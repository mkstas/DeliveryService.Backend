using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.AuthService.Permissions
{
    /// <summary>
    /// Represents an authorization requirement that the current user holds the specified permissions.
    /// </summary>
    /// <param name="permissionSystemNames">The system names of the required permissions.</param>
    public class PermissionRequirement(string[] permissionSystemNames) : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets the system names of the required permissions.
        /// </summary>
        public string[] PermissionSystemNames { get; } = permissionSystemNames ?? [];
    }
}
