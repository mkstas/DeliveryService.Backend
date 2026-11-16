using Domain.Entities.Catalog;
using Domain.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Persistance database seeder.
    /// </summary>
    internal static class PersistenceSeeder
    {
        /// <summary>
        /// Seeds the database.
        /// </summary>
        /// <param name="context">The database context to seed.</param>
        public static async Task SeedAsync(PersistenceDbContext context)
        {
            var existingPermissions = await context.Permissions.ToListAsync();
            var existingNames = existingPermissions.Select(p => p.SystemName).ToHashSet();

            var missingPermissions = Permissions.All
                .Where(d => !existingNames.Contains(d.SystemName))
                .Select(d => Permission.Create(d.SystemName, d.DisplayName))
                .ToList();

            if (missingPermissions.Count > 0)
            {
                await context.Permissions.AddRangeAsync(missingPermissions);
                await context.SaveChangesAsync();
            }

            var allPermissions = await context.Permissions.ToListAsync();

            var administrator = await SeedRoleAsync(context, Roles.Administrator);
            await SeedRoleAsync(context, Roles.Client);
            await SeedRoleAsync(context, Roles.Manager);

            await SeedAdministratorPermissionsAsync(administrator, allPermissions);

            await context.SaveChangesAsync();
        }

        private static async Task<Role> SeedRoleAsync(PersistenceDbContext context, string roleName)
        {
            var role = await context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == roleName);

            if (role is null)
            {
                role = Role.Create(roleName, true);
                context.Roles.Add(role);
            }

            return role;
        }

        private static Task SeedAdministratorPermissionsAsync(Role administrator, List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                if (administrator.Permissions.All(p => p.Id != permission.Id))
                {
                    administrator.AddPermission(permission);
                }
            }

            return Task.CompletedTask;
        }
    }
}
