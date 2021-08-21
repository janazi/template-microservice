using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Reflection;
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
            if (IsWarmedUp is true)
                return Task.FromResult(HealthCheckResult.Healthy());

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.Contains(AppDomain.CurrentDomain.FriendlyName)))
                assembly.GetTypes().Where(myType => myType.GetInterfaces().Contains(typeof(IWarmUpController))).ToList().ForEach(t =>
                {
                    var controller = (IWarmUpController)serviceProvider.GetRequiredService(t);
                    controller.WarmUp();
                });

            IsWarmedUp = true;

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
