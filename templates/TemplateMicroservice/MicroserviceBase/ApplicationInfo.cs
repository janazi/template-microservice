using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace MicroserviceBase
{
    public static class ApplicationInfo
    {
        private static string ServiceName;
        private static string ServiceId;

        public static void Configure(IConfiguration configuration)
        {
            var msSettings = configuration.GetSection("MicroserviceSettings");
            if (msSettings is null)
                throw new ArgumentException("Missing MicroserviceSettings configuration section");

            var version = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            ServiceName = msSettings["Name"];
            ServiceId = $"{ServiceName}-{version}";
        }

        public static string GetServiceName() => ServiceName;
        public static string GetServiceId() => ServiceId;
    }
}
