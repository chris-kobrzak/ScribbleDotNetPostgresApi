using Microsoft.Extensions.Configuration;
using Oss.Api;
using Oss.Client.Database;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthConfig>(config.GetSection("Auth"));
            services.Configure<PostgresClientConfig>(config.GetSection("Database"));

            return services;
        }
    }
}