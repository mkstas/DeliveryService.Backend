using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Persistence.Postgres
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistencePostgres(
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddDbContext<DeliveryServiceDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
