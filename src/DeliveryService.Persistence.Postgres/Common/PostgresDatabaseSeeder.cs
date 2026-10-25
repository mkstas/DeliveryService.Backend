using DeliveryService.Domain.Common;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres.Common
{
    /// <summary>
    /// Seeds the database.
    /// </summary>
    public class PostgresDatabaseSeeder(DeliveryServiceDbContext context)
    {
        private const string AdministratorRoleName = "Administrator";

        /// <summary>
        /// Seeds the database idempotently.
        /// </summary>
        public async Task SeedAsync()
        {
            var permissions = await SeedPermissionsAsync();
            var administrator = await SeedRoleAsync(AdministratorRoleName);

            await SeedAdministratorPermissionsAsync(administrator, permissions);
            await context.SaveChangesAsync();
        }

        private async Task<List<Permission>> SeedPermissionsAsync()
        {
            var existing = await context.Permissions.ToListAsync();
            var existingNames = existing.Select(p => (string)p.SystemName).ToHashSet();

            var missing = Permissions.All
                .Where(d => !existingNames.Contains(d.SystemName))
                .Select(d => Permission.Create(
                    StringBounded.Create(d.SystemName),
                    StringBounded.Create(d.DisplayName)))
                .ToList();

            context.Permissions.AddRange(missing);
            existing.AddRange(missing);

            return existing;
        }

        private async Task<Role> SeedRoleAsync(string roleName)
        {
            var role = await context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == StringBounded.Create(roleName));

            if (role is null)
            {
                role = Role.Create(StringBounded.Create(roleName));
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
