using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Repositories
{
    public class RoleRepository(DeliveryServiceDbContext context) : IRoleRepository
    {
        public async Task CreateRoleAsync(Role role)
        {
            context.Roles.Add(role);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Permission>(role.Name);
            }
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await context.Roles
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new AlreadyExistsException<Permission>(role.Name);
            }
        }
    }
}
