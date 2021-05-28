using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace MicroserviceBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(opt =>
                    {
                        opt.AddServerHeader = false;
                        opt.Listen(IPAddress.Loopback, 5096, cfg =>
                        {
                            cfg.Protocols = HttpProtocols.Http1AndHttp2;
                        });
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
