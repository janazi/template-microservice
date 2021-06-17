using MicroserviceBase.Domain.Policies;
using MicroserviceBase.Infrastructure.Bootstrap.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceBase.Infrastructure.Bootstrap
{
    public class ApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            //    options.JsonSerializerOptions.IgnoreNullValues = true;
            //});

            services.AddApiVersion();
            services.AddCustomHealthChecks();
            services.AddSingleton<DatabasePolicies>();
            //services.AddOpenTracing();
            //services.AddJaegerHttpTracer(configuration);
            //services.AddMassTransitOpenTracing();

            // var rabbitOptions = new  RabbitConfig { Host = "10.30.2.15", Password = "m5i3g7r3a1t7i8o9n0", Username = "mquser_mig", VirtualHost= "migration" };
            //services.AddMassTransit(x =>
            //{
            //    //events
            //    x.AddConsumers(typeof(CardDataReadyEventConsumer), typeof(CardDataReadyEventConsumerDefinition));
            //    x.AddConsumers(typeof(PersonDataReadyEventConsumer), typeof(PersonDataReadyEventConsumerDefinition));
            //    x.AddConsumers(typeof(AccountDataReadyEventConsumer), typeof(AccountDataReadyEventConsumerDefinition));
            //    //commands
            //    x.AddConsumers(typeof(StartMigrationConsumer), typeof(StartMigrationConsumerDefinition));

            //    MassTransitConfigurations.AddEndPointConventions();
            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            //    {
            //        cfg.Host("10.30.2.15", "migration", h =>
            //        {
            //            h.Username("mquser_mig");
            //            h.Password("m5i3g7r3a1t7i8o9n0");
            //        });
            //        cfg.ConfigureEndpoints(provider);
            //    }));
            //});

            //services.AddMassTransitHostedService();
            //services.AddHostedService<BusService>();

            //var mongoDbConnection = @"mongodb://orchestratorAdmin:QjYFYiUfQUI0p033CPP5@mbr-orchestrator-docdb-dev.cluster-chnniibdgfzs.us-east-1.docdb.amazonaws.com:27017/?ssl=true&ssl_ca_certs=rds-combined-ca-bundle.pem&replicaSet=rs0&readPreference=secondaryPreferred&retryWrites=false";
            //services.AddSingleton(new MongoClient(mongoDbConnection));

            //services.AddScoped<IMigrationRepository, MigrationRepository>();
            //services.AddScoped<IPersonDataReadyUseCase, PersonDataReadyUseCase>();
            //services.AddScoped<IAccountDataReadyUseCase, AccountDataReadyUseCase>();
            //services.AddScoped<ICardsDataReadyUseCase, CardsDataReadyUseCase>();

            //services.AddScoped<IAuthRepository, AuthRepository>();
            //services.AddScoped<IAuthUseCase, AuthUseCase>();
            //services.AddScoped<IAuthInfoUseCase, AuthInfoUseCase>();

            //services.AddJaeger(configuration);
            //services.AddSuperSwagger(new OpenApiInfo()
            //{
            //    Title = "Serviço",
            //    Description = "Descrição do serviço",
            //}, configuration);

            services.AddCustomResponseCompression();
            //services.AddMetrics();
            //ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        //{
        //    app.UseSuperSwagger(provider);
        //    app.UseCustomHealthChecks();
        //    app.UseResponseCompression();
        //    app.UseRouting();
        //    app.UseEndpoints(ep => ep.MapControllers());
        //}
    }
}
