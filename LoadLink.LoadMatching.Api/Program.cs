using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Sentry;
using Serilog.Events;

namespace LoadLink.LoadMatching.Api
{
 public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostEnvironment env = null;

            var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();
                env = hostingContext.HostingEnvironment;

                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();

                if (env.IsDevelopment())
                {
                    // will use this if we have secret keys
                    config.AddUserSecrets<Startup>();
                }

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .UseSerilog((context, config) =>
            {
               config.Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Sentry(o =>
                {
                    // Debug and higher are stored as breadcrumbs (default is Information)
                    o.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    // Warning and higher is sent as event (default is Error)
                    o.MinimumEventLevel = LogEventLevel.Warning;
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                _ = webBuilder.ConfigureKestrel(serverOptions =>
                  {
                      serverOptions.AddServerHeader = false;
                  })
                .UseSentry(o =>
                {
                    o.AddInAppInclude("Serilog");
                    o.BeforeSend = @event =>
                    {
                        // Never report server name(s)
                        @event.ServerName = null;
                        return @event;
                    };
                })
                .UseStartup<Startup>();
            });

            return hostBuilder;

        }
    }
}
