using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Provides read/write access to permissions.
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Returns a permission by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <returns>The matching permission, or <c>null</c> if none found.</returns>
        Task<Permission?> GetPermissionByIdAsync(Guid permissionId);

        /// <summary>
        /// Returns all permissions in the system.
        /// </summary>
        /// <returns>All permissions in the system.</returns>
        Task<List<Permission>> GetPermissionsAsync();

        /// <summary>
        /// Returns all permissions assigned to a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>All permissions assigned to the user.</returns>
        Task<List<Permission>> GetUserPermissionsByUserIdAsync(Guid userId);

        /// <summary>
        /// Updates the specified permission.
        /// </summary>
        /// <param name="permission">The permission to update.</param>
        Task UpdatePermissionAsync(Permission permission);
    }
}
