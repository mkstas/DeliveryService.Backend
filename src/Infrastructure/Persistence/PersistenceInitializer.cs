using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Applies pending migrations and seeds the database on startup.
    /// </summary>
    public class PersistenceInitializer()
    {
        /// <summary>
        /// Applies pending migrations and seeds the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider to resolve the database context from.</param>
        public static async Task InitAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<PersistenceDbContext>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            await context.Database.MigrateAsync();
            await PersistenceSeeder.SeedAsync(context, configuration);
        }
    }
}
