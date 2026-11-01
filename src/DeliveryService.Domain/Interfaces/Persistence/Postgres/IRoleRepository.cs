using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;

namespace DeliveryService.Domain.Interfaces.Persistence.Postgres
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
        /// Returns a role by its name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="name">The role name.</param>
        /// <returns>The matching role, or <c>null</c> if none found.</returns>
        Task<Role?> GetRoleByNameAsync(StringBounded name);

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
