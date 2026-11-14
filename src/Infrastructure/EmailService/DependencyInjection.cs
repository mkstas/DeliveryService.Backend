using Application.Abstractions.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EmailService
{
    /// <summary>
    /// Provides dependency injection for the email services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers email dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the email dependencies.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to get smtp options.</param>
        /// <returns>The same service collection for fluent method chaining.</returns>
        public static IServiceCollection AddEmailService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
