using FluentValidation.AspNetCore;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoadLink.LoadMatching.Api
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
            services.Configure<AppSettings>(Configuration);

            services.AddHttpContextAccessor();

            services.RegisterAuthentication();

            services.AddSingleton(Configuration);

            services.AllowedCors();

            services.AddControllers(setup =>
            {
                setup.InputFormatters.Insert(0, StartupHelper.GetJsonPatchInputFormatter());
            }).AddFluentValidation();

            services.AddValidators();

            services.AddSwagger();

            services.AddApplicationServices(Configuration);

            services.AddCache();
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

            app.UseHttpsRedirection();

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
