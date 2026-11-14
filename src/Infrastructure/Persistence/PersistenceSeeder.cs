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
            var permissions = await context.Permissions.ToListAsync();
            var permissionsName = permissions.Select(p => p.SystemName).ToHashSet();

            var missingPermissions = Permissions.All
                .DistinctBy(d => d.SystemName)
                .Where(d => !permissionsName.Contains(d.SystemName))
                .Select(d => Permission.Create(d.SystemName, d.DisplayName))
                .ToList();

            await context.Permissions.AddRangeAsync(missingPermissions);
            await context.SaveChangesAsync();
        }
    }
}
