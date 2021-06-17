using MassTransit;
using MicroserviceBase.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            EndpointConvention.Map<AddCustomerCommand>(
                EndpointAdressProvider.GetEndPointAdress<AddCustomerCommand>());
        }

        public class EndpointAdressProvider
        {
            public static Uri GetEndPointAdress<T>(AddressType addressType = AddressType.Queue)
            {
                var type = typeof(T);
                return new Uri(addressType.ToString().ToLower() + ":" + type.Namespace + ":" + type.Name);
            }
        }

        public enum AddressType
        {
            Queue,
            Exchange,
            Topic,
        }
    }
}
