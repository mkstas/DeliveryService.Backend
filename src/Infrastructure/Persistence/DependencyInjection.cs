using Domain.Abstractions.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Provides dependency injection for the persistence services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers persistence dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the persistence dependencies.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to get connection string.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddPersistenceService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PersistenceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(nameof(PersistenceDbContext))));

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
