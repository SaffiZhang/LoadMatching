using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LoadLink.LoadMatching.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.Sources.Clear();

                 var env = hostingContext.HostingEnvironment;

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
                 .Enrich.WithMachineName()
                 .WriteTo.Console();
             })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
}
