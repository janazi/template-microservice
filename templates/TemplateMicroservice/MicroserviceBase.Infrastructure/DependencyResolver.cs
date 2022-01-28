using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase.Infrastructure.CrossCutting
{
    public static class DependencyResolver
    {
        internal static IServiceProvider Instance { get; private set; }
        public static IServiceCollection Services { get; private set; }

        public static void SetServiceCollection(IServiceCollection services)
        {
            if (Services is not null)
                return;

            Services = services;
        }

        public static void SetServiceProvider(IServiceProvider services)
        {
            if (Instance is not null)
                return;

            Instance = services;
        }

        public static T GetService<T>()
            where T : class
        {
            return Instance.GetService<T>();
        }
    }
}
