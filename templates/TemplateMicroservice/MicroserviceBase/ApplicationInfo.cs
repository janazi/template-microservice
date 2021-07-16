﻿using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace MicroserviceBase
{
    public static class ApplicationInfo
    {
        private static string _serviceName;
        private static string _serviceId;
        private static string _version;

        public static void Configure(IConfiguration configuration)
        {
            var msSettings = configuration.GetSection("MicroserviceSettings");
            if (msSettings is null)
                throw new ArgumentException("Missing MicroserviceSettings configuration section");

            _version = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            _serviceName = msSettings["Name"];
            _serviceId = $"{_serviceName}-{_version}";
        }

        public static string GetServiceName() => _serviceName;
        public static string GetServiceId() => _serviceId;
        public static string GetVersion() => _version;
    }
}
