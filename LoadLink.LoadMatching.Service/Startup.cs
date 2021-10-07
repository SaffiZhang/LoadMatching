using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry.AspNetCore;
using AutoMapper;
using System;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.RabbitMQIntegrationEventManager;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
using LoadLink.LoadMatching.Persistence.Data;
using MediatR;
using LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
namespace LoadLink.LoadMatching.MatchingService
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
            // connections
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IConnectionFactory>(x => new ConnectionFactory(connectionString));

            services.AddSingleton(Configuration);


            // mapping profiles            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Changes in 2021-9-21
            var matchingConfig = Configuration.GetSection("MatchingConfig").Get<MatchingConfig>();
            services.AddSingleton<IMatchingConfig>(matchingConfig);

            var MqConfig = Configuration.GetSection("MqConfig").Get<MqConfig>();
            services.AddSingleton(MqConfig);

            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();

            services.AddMediatR(typeof(PostingBase).Assembly, typeof(CreateEquipmentPostingCommandHandler).Assembly, typeof(ConsoleService.LoadMatchingService).Assembly);
            services.AddTransient<IFillNotPlatformPosting, FillingNotPlatformPosting>();

            services.AddTransient<IMatchingServiceFactory, MatchingServiceFactory>();
            services.AddSingleton<LoadMatchingService>();
            services.AddSingleton<IIntegrationEventHandler<PostingCreatedEvent>, PostingCreatedEventHandler>();
            services.AddSingleton<IIntegationEventHandlerRegister<PostingCreatedEvent>, IntegrationHandlerRegister<PostingCreatedEvent, PostingCreatedEventHandler>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          

         
            app.UseSentryTracing();

            
        }
    }
}
