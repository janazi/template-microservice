using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase.Infrastructure.CrossCutting
{
    public static class DependencyResolver
    {
        internal static IServiceProvider Instance { get; private set; }

        public static void SetServiceCollection(IServiceProvider services)
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
