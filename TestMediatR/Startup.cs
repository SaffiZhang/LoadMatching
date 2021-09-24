using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using ClassLibrary2;
using LoadLink.LoadMatching.Domain.AggregatesModel.BasePostingAggregate;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
using LoadLink.LoadMatching.Persistence.Data;

namespace TestMediatR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server = udvlpdb01.linklogi.com; Database = LoadMatching; Trusted_Connection = yes;";

            services.AddSingleton<IConnectionFactory>(x => new ConnectionFactory(connectionString));
            services.AddMediatR(typeof(TestCommand).Assembly, typeof (Startup).Assembly);
            services.AddControllers();
            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
