using Domain.Entities.Catalog;

namespace Domain.Abstractions.Repositories
{
    /// <summary>
    /// Provides read/write access to permissions.
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Returns all permissions in the system.
        /// </summary>
        /// <returns>All permissions in the system.</returns>
        Task<IReadOnlyList<Permission>> GetPermissionsAsync();

        /// <summary>
        /// Returns all permissions assigned to a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>All permissions assigned to the user.</returns>
        Task<IReadOnlyList<Permission>> GetUserPermissionsByUserIdAsync(Guid userId);

        /// <summary>
        /// Returns a permission by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="permissionId">The permission identifier.</param>
        /// <returns>The matching permission, or <c>null</c> if none found.</returns>
        Task<Permission?> GetPermissionByIdAsync(Guid permissionId);
    }
}
