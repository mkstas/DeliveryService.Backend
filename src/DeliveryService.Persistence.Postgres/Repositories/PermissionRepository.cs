using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class PermissionRepository(DeliveryServiceDbContext context) : IPermissionRepository
    {
        public async Task<Permission?> GetPermissionByIdAsync(Guid permissionId)
        {
            return await context.Permissions.FirstOrDefaultAsync(p => p.Id == permissionId);
        }

        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return await context.Permissions.ToListAsync();
        }

        public async Task<List<Permission>> GetUserPermissionsByUserIdAsync(Guid userId)
        {
            return await context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .SelectMany(r => r.Permissions)
                .Distinct()
                .ToListAsync();
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Permission>(permission.DisplayName);
            }
        }
    }
}
