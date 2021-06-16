using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceBase.Infrastructure.Messaging
{
    public static class MasstransitConsumersExtesions
    {
        public static void ConfigureMasstransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageConsumer>();
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService(true);
        }
    }
}
