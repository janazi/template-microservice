using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceBase.Infrastructure.Bootstrap.Extensions
{
    public static class HealthChecksServiceCollectionExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }
    }
}
