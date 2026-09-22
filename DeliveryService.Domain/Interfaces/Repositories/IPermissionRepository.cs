using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Provides read/write acces to permissions.
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Returns a permission by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        Task<Permission?> GetPermissionByIdAsync(Guid permissionId);

        /// <summary>
        /// Returns all permissions in the system.
        /// </summary>
        Task<List<Permission>> GetPermissionsAsync();

        /// <summary>
        /// Return all permissions assigned to a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task<List<Permission>> GetUserPermissionsByUserIdAsync(Guid userId);

        /// <summary>
        /// Update the specified permission.
        /// </summary>
        /// <param name="permission">The permission to update.</param>
        Task UpdatePermissionAsync(Permission permission);
    }
}
