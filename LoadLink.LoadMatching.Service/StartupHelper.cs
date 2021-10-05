
using AutoMapper;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;


using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;

using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;

using LoadLink.LoadMatching.IntegrationEventManager;



namespace LoadLink.LoadMatching.Service
{
    public static class StartupHelper
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // connections
            var connectionString = configuration.GetConnectionString("DefaultConnection");
           
        
            
            // mapping profiles            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

          
            //Changes in 2021-9-21
            
            var matchingConfig = configuration.GetSection("MatchingConfig").Get<MatchingConfig>();
            services.AddSingleton<IMatchingConfig>(matchingConfig);

            var MqConfig = configuration.GetSection("MqConfig").Get<MqConfig>();
            services.AddSingleton(MqConfig);

            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();

            services.AddMediatR(typeof(PostingBase).Assembly, typeof(CreateEquipmentPostingCommandHandler).Assembly, typeof(Startup).Assembly);
            services.AddTransient<IFillNotPlatformPosting, FillingNotPlatformPosting>();

            services.AddTransient<IMatchingServiceFactory, MatchingServiceFactory>();
         
            services.AddHostedService<LoadMatchingService>();



        }

    }
}
