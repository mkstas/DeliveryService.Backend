using Application.DTOs.Permissions;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing permissions.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Returns all permissions in the system.
        /// </summary>
        /// <returns>All permissions in the system.</returns>
        Task<IReadOnlyList<PermissionResponse>> GetPermissionsAsync();
    }
}
