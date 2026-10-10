using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides operations for managing roles.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="name">The name of the role to create.</param>
        Task CreateRoleAsync(StringBounded name);

        /// <summary>
        /// Returns a role by its identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The matching role.</returns>
        Task<Role> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Returns all roles in the system.
        /// </summary>
        /// <returns>All roles in the system.</returns>
        Task<List<Role>> GetRolesAsync();

        /// <summary>
        /// Updates the name of the specified role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="newName">The new name of the role.</param>
        Task UpdateRoleAsync(Guid roleId, StringBounded newName);

        /// <summary>
        /// Assigns a permission to the specified role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="permissionId">The permission identifier.</param>
        Task AddRolePermissionAsync(Guid roleId, Guid permissionId);

        /// <summary>
        /// Removes a permission from the specified role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="permissionId">The permission identifier.</param>
        Task RemoveRolePermissionAsync(Guid roleId, Guid permissionId);
    }
}
