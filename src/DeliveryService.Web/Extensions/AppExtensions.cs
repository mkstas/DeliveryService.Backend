using DeliveryService.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using DeliveryService.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace DeliveryService.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring the web application pipeline.
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Adds the <see cref="ExceptionHandler"/> middleware to the application pipeline.
        /// </summary>
        /// <param name="app">The web application to configure.</param>
        public static void UseExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandler>();
        }

        /// <summary>
        /// Maps the OpenAPI document and Scalar API reference in the development environment.
        /// </summary>
        /// <param name="app">The web application to configure.</param>
        public static void MapDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
        }

        /// <summary>
        /// Applies pending database migrations and seeds the PostgreSQL database.
        /// </summary>
        /// <param name="app">The web application whose database is migrated and seeded.</param>
        /// <returns>A task that represents the asynchronous seeding operation.</returns>
        public static async Task SeedPostgresDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DeliveryServiceDbContext>();

            context.Database.Migrate();
            await new PostgresDatabaseSeeder(context).SeedAsync();
        }
    }
}
