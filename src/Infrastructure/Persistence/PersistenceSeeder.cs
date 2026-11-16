using Application.Common;
using Domain.Abstractions.Permissions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        /// <param name="configuration">The application configuration.</param>
        public static async Task SeedAsync(PersistenceDbContext context, IConfiguration configuration)
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

            var administrator = await SeedRoleAsync(context, RoleNames.Administrator);
            await SeedRoleAsync(context, RoleNames.Client);
            await SeedRoleAsync(context, RoleNames.Manager);

            await SeedAdministratorPermissionsAsync(administrator, allPermissions);
            await SeedAdministratorUserAsync(context, configuration, administrator);

            await context.SaveChangesAsync();
        }

        private static async Task SeedAdministratorUserAsync(
            PersistenceDbContext context,
            IConfiguration configuration,
            Role administratorRole)
        {
            var section = configuration.GetSection("AdminSeedOptions");
            var emailAddress = section["EmailAddress"];
            var password = section["Password"];

            if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            var existingUser = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);

            if (existingUser is null)
            {
                var adminUser = User.Create(emailAddress, PasswordHasher.Generate(password), "Администратор");
                adminUser.AddRole(administratorRole);
                context.Users.Add(adminUser);
                return;
            }

            if (existingUser.Roles.All(r => r.Id != administratorRole.Id))
            {
                existingUser.AddRole(administratorRole);
            }
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
