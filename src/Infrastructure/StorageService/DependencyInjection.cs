using Amazon.S3;
using Application.Abstractions.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.StorageService
{
    /// <summary>
    /// Provides dependency injection for the storage services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers storage dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the storage dependencies.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to get storage options.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddStorageService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var storageOptions = configuration.GetSection(nameof(StorageOptions))
                ?? throw new InvalidOperationException($"{nameof(StorageOptions)} not configured.");

            services.Configure<StorageOptions>(storageOptions);

            services.AddSingleton<IAmazonS3>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<StorageOptions>>().Value;

                return new AmazonS3Client(
                    options.AccessKey,
                    options.SecretKey,
                    new AmazonS3Config
                    {
                        ServiceURL = options.ServiceURL,
                        ForcePathStyle = true
                    });
            });

            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}
