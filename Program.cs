using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace APP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.Limits.MaxConcurrentConnections = 100;
                options.Limits.MaxConcurrentUpgradedConnections = 100;
                options.Limits.MaxRequestBodySize = 10 * 1024;
                options.Limits.MinRequestBodyDataRate =
                    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                options.Limits.MinResponseDataRate =
                    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                options.Listen(IPAddress.Loopback, 5000);
                options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                {
                    listenOptions.UseHttps("Certificat/certificate.pfx", "46f8592d63d59c348fc240a4778eb708");
                });
            })
              .UseStartup<Startup>()
              .Build();
    }
}
