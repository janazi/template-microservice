using Jaeger.Propagation;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Util;
using System;
using JaegerTracer = Jaeger.Tracer;

namespace MicroserviceBase.Infrastructure.Bootstrap.Extensions
{
    public static class JaegerServiceCollectionExtensions
    {
        public static IServiceCollection AddJaeger(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));


            services.AddSingleton<ITracer>((serviceProvider) =>
            {
                var logger = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

                services.Configure<OpenTracingConfigurations>(configuration.GetSection(nameof(OpenTracingConfigurations)));
                var options = services.BuildServiceProvider().GetRequiredService<IOptions<OpenTracingConfigurations>>().Value;

                if (options == null || string.IsNullOrEmpty(options.ServiceName))
                {
                    throw new ArgumentNullException("Arguments for Jaeger Configuration found");
                }

                if (!options.EnableTracing)
                {
                    ConstSampler sampler = new ConstSampler(false);

                    var tracer = new JaegerTracer.Builder(options.ServiceName).WithSampler(sampler).Build();
                    return tracer;
                }
                else
                {
                    ISender sender = new HttpSender(options.SenderEndpoint);

                    var reporter = new RemoteReporter.Builder()
                        .WithSender(sender)
                        .WithLoggerFactory(logger)
                        .Build();


                    ISampler sampler = new ConstSampler(sample: true);

                    var tracer = new JaegerTracer.Builder(options.ServiceName)
                        .WithSampler(sampler)
                        .WithReporter(reporter)
                        .RegisterCodec(BuiltinFormats.HttpHeaders, new B3TextMapCodec())
                        .Build();

                    GlobalTracer.Register(tracer);

                    return tracer;
                }
            });

            return services;
        }
    }

    public class OpenTracingConfigurations
    {
        public string ServiceName { get; set; }
        public bool EnableTracing { get; set; }
        public string SenderEndpoint { get; set; }
    }
}