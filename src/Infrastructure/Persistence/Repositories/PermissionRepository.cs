using Domain.Abstractions.Repositories;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IPermissionRepository"/>.
    /// </summary>
    internal class PermissionRepository(PersistenceDbContext context) : IPermissionRepository
    {
        /// <inheritdoc />
        public async Task<IReadOnlyList<Permission>> GetPermissionsAsync()
        {
            return await context.Permissions.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Permission>> GetUserPermissionsByUserIdAsync(Guid userId)
        {
            return await context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .SelectMany(r => r.Permissions)
                .Distinct()
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Permission?> GetPermissionByIdAsync(Guid permissionId)
        {
            return await context.Permissions.FirstOrDefaultAsync(p => p.Id == permissionId);
        }
    }
}
