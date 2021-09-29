using FluentValidation.AspNetCore;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry.AspNetCore;
using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using System.ComponentModel;

namespace LoadLink.LoadMatching.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

  
            services.Configure<AppSettings>(Configuration);

            services.AddHttpContextAccessor();

            services.RegisterAuthentication();

            services.AddSingleton(Configuration);

            services.AllowedCors();

            services.AddControllers(setup =>
            {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                setup.Filters.Add(new AuthorizeFilter(policy));
                setup.InputFormatters.Insert(0, StartupHelper.GetJsonPatchInputFormatter());
            }).AddFluentValidation();

            services.AddMvc();

            services.AddValidators();

            services.AddSwagger();

            services.AddApplicationServices(Configuration);

            services.AddRedisCaching(Configuration);
            
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowedCorsOrigin");

            app.UseFluentValidationExceptionHandler();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LoadLink LoadMatching API");
                options.OAuthAppName("linkup");

                options.RoutePrefix = "swagger/ui";
            });

            //app.UseHttpsRedirection();

            app.UseSentryTracing();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
