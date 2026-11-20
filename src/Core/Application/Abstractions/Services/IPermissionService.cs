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

        /// <summary>
        /// Returns the system names of all permissions granted to the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The system names of the user permissions.</returns>
        Task<IReadOnlyList<string>> GetUserPermissionNamesAsync(Guid userId);
    }
}
