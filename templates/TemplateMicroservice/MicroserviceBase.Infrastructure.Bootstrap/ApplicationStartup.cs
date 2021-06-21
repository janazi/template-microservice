using CorrelationIdRequestHeader;
using MediatR;
using MicroserviceBase.Domain.Policies;
using MicroserviceBase.Infrastructure.Bootstrap.Extensions;
using MicroserviceBase.Infrastructure.CrossCutting;
using MicroserviceBase.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace MicroserviceBase.Infrastructure.Bootstrap
{
    public class ApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, string applicationName)
        {
            ConfigureLogging(configuration, applicationName);
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.ConfigureMasstransit();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroserviceBase", Version = "v1" });
            });
            services.AddApiVersion();
            services.AddCustomHealthChecks();
            services.AddSingleton<DatabasePolicies>();
            services.AddMediatR(AppDomain.CurrentDomain.Load("MicroserviceBase.Application"));
            //services.AddOpenTracing();
            //services.AddJaegerHttpTracer(configuration);
            //services.AddMassTransitOpenTracing();
            //var mongoDbConnection = @"mongodb://orchestratorAdmin:QjYFYiUfQUI0p033CPP5@mbr-orchestrator-docdb-dev.cluster-chnniibdgfzs.us-east-1.docdb.amazonaws.com:27017/?ssl=true&ssl_ca_certs=rds-combined-ca-bundle.pem&replicaSet=rs0&readPreference=secondaryPreferred&retryWrites=false";
            //services.AddSingleton(new MongoClient(mongoDbConnection));

            //services.AddSuperSwagger(new OpenApiInfo()
            //{
            //    Title = "Serviço",
            //    Description = "Descrição do serviço",
            //}, configuration);

            services.AddCustomResponseCompression();
            //services.AddMetrics();
        }

        private static void ConfigureLogging(IConfiguration configuration, string applicationName)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", environment)
                .Enrich.WithProperty("AppllicationName", applicationName)
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss:ms} [{Level}] {Properties}: {NewLine}{Message}{NewLine}{Exception}{NewLine}")
                .Enrich.WithCorrelationIdHeader("x-correlation-id")
                //.WriteTo.Elasticsearch(ConfigureElasticSink(context.Configuration, context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var versionDescription in (IEnumerable<ApiVersionDescription>)provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint("./" + versionDescription.GroupName + "/swagger.json", versionDescription.GroupName.ToLowerInvariant());
                    c.OAuthClientId(string.Empty);
                    c.OAuthClientSecret(string.Empty);
                }
            });


            app.AddRequestHeaderCorrelationId();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(ep => ep.MapControllers());
            app.UseResponseCompression();

            DependencyResolver.SetServiceCollection(app.ApplicationServices);
        }
    }
}
