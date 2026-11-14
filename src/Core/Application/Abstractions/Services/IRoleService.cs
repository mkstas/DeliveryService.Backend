using Application.DTOs.Roles;

namespace Application.Abstractions.Services
{
    /// <summary>
    /// Provides operations for managing roles.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="request">The request to create role.</param>
        Task CreateRoleAsync(CreateRoleRequest request);

        /// <summary>
        /// Returns all roles in the system.
        /// </summary>
        /// <returns>All roles in the system.</returns>
        Task<IReadOnlyList<RoleResponse>> GetRolesAsync();

        /// <summary>
        /// Returns a role by its identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The matching role.</returns>
        Task<RoleResponse> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Updates the name of the specified role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="request">The request to update role.</param>
        Task UpdateRoleAsync(Guid roleId, UpdateRoleRequest request);

        /// <summary>
        /// Deletes the specified role from the system.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        Task DeleteRoleAsync(Guid roleId);

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
