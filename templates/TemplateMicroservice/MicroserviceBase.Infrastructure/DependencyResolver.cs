using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase.Infrastructure.CrossCutting
{
    public static class DependencyResolver
    {
        private static IServiceProvider Instance { get; set; }

        public static void SetServiceCollection(IServiceProvider services)
        {
            Instance = services;
        }

        public static T GetService<T>()
            where T : class
        {
            return Instance.GetService<T>();
        }
    }
}
