using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceBase.Infrastructure.CrossCutting.HealthChecks
{
    public class WarmupHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider serviceProvider;

        private static bool IsWarmedUp { get; set; }

        public WarmupHealthCheck(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
            if (IsWarmedUp is true)
                return Task.FromResult(HealthCheckResult.Healthy());

            foreach (var service in DependencyResolver.Services)
            {
                try
                {
                    var s = serviceProvider.GetService(service.ServiceType);
                }
                catch { }
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.Contains(AppDomain.CurrentDomain.FriendlyName));
            foreach (var assembly in assemblies)
            {
                assembly.GetTypes().Where(t => t.FullName.Contains("Controller")).ToList()
                    .ForEach(t =>
                    {
                        try
                        {
                            _ = serviceProvider.GetRequiredService(t);
                        }
                        catch { }
                    });
            }

            IsWarmedUp = true;

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
