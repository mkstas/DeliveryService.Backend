using Application.Common;
using Domain.Abstractions.Permissions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Persistence database seeder.
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
            var client = await SeedRoleAsync(context, RoleNames.Client);
            var manager = await SeedRoleAsync(context, RoleNames.Manager);

            SeedRolePermissions(administrator, Permissions.DefaultSets.Administrator, allPermissions);
            SeedRolePermissions(client, Permissions.DefaultSets.Client, allPermissions);
            SeedRolePermissions(manager, Permissions.DefaultSets.Manager, allPermissions);

            await SeedAdministratorUserAsync(context, configuration, administrator);

            await context.SaveChangesAsync();

            await DemoDataSeeder.SeedAsync(context);
        }

        private static async Task SeedAdministratorUserAsync(
            PersistenceDbContext context,
            IConfiguration configuration,
            Role administratorRole)
        {
            var section = configuration.GetSection("AdministratorOptions");
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

        private static void SeedRolePermissions(Role role, IEnumerable<string> systemNames, IReadOnlyList<Permission> permissions)
        {
            var desiredNames = systemNames.ToHashSet();
            var permissionsByName = permissions.ToDictionary(p => p.SystemName);

            foreach (var permission in role.Permissions.ToList())
            {
                if (!desiredNames.Contains(permission.SystemName))
                    role.RemovePermission(permission);
            }

            foreach (var systemName in desiredNames)
            {
                if (permissionsByName.TryGetValue(systemName, out var permission)
                    && role.Permissions.All(p => p.Id != permission.Id))
                {
                    role.AddPermission(permission);
                }
            }
        }
    }
}
