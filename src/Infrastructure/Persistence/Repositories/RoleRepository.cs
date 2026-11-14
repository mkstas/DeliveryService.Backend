using Domain.Abstractions.Repositories;
using Domain.Entities.Catalog;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IRoleRepository"/>.
    /// </summary>
    internal class RoleRepository(PersistenceDbContext context) : IRoleRepository
    {
        /// <inheritdoc />
        public async Task CreateRoleAsync(Role role)
        {
            context.Roles.Add(role);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Role>(role.Name);
            }
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Role>> GetRolesAsync()
        {
            return await context.Roles
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        /// <inheritdoc />
        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            return await context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        /// <inheritdoc />
        public async Task UpdateRoleAsync(Role role)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
            {
                throw new ConflictException<Role>(role.Name);
            }
        }

        /// <inheritdoc />
        public async Task RemoveRoleAsync(Role role)
        {
            context.Roles.Remove(role);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
            {
                throw new DeleteConstraintException<Role>("assigned to users or permissions");
            }
        }
    }
}
