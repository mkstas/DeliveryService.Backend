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
        /// <summary>
        /// Seeds the database idempotently.
        /// </summary>
        public async Task SeedAsync()
        {
            var permissions = await SeedPermissionsAsync();

            var administrator = await SeedRoleAsync(RoleNames.Administrator);
            var manager = await SeedRoleAsync(RoleNames.Manager);
            var client = await SeedRoleAsync(RoleNames.Client);

            await SeedAdministratorPermissionsAsync(administrator, permissions);
            await SeedManagerPermissionsAsync(manager, permissions);
            await SeedClientPermissionsAsync(client, permissions);

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

        private static Task SeedManagerPermissionsAsync(Role manager, List<Permission> permissions)
        {
            var availablePermissions = new[]
            {
                Permissions.Establishments.Update,
                Permissions.Ingredients.Create,
                Permissions.Ingredients.Update,
                Permissions.Modifiers.Create,
                Permissions.Modifiers.Update,
                Permissions.Dishes.Create,
                Permissions.Dishes.Update
            };

            foreach (var availablePermission in availablePermissions)
            {
                var permission = permissions.FirstOrDefault(p => (string)p.SystemName == availablePermission);

                if (permission is not null && manager.Permissions.All(p => p.Id != permission.Id))
                {
                    manager.AddPermission(permission);
                }
            }

            return Task.CompletedTask;
        }

        private static Task SeedClientPermissionsAsync(Role client, List<Permission> permissions)
        {
            var availablePermissions = new[]
            {
                Permissions.Carts.Create,
                Permissions.Carts.Read,
                Permissions.Carts.Update,
            };

            foreach (var availablePermission in availablePermissions)
            {
                var permission = permissions.FirstOrDefault(p => (string)p.SystemName == availablePermission);

                if (permission is not null && client.Permissions.All(p => p.Id != permission.Id))
                {
                    client.AddPermission(permission);
                }
            }

            return Task.CompletedTask;
        }
    }
}
