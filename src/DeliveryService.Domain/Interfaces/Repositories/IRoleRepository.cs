using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Provides read/write access to roles.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="role">The role to create.</param>
        Task CreateRoleAsync(Role role);

        /// <summary>
        /// Returns a role by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The matching role, or <c>null</c> if none found.</returns>
        Task<Role?> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Returns all roles in the system.
        /// </summary>
        /// <returns>All roles in the system.</returns>
        Task<List<Role>> GetRolesAsync();

        /// <summary>
        /// Updates the specified role.
        /// </summary>
        /// <param name="role">The role to update.</param>
        Task UpdateRoleAsync(Role role);
    }
}
