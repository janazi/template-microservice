using MediatR;
using MicroserviceBase.Infrastructure.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            startup = new ApplicationStartup();
        }

        public IConfiguration Configuration { get; }
        private readonly ApplicationStartup startup;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationInfo.Configure(Configuration);
            startup.ConfigureServices(services, Configuration, ApplicationInfo.GetServiceId());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiDesProv)
        {
            startup.Configure(app, env, apiDesProv);
        }
    }
}
