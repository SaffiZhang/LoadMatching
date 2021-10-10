using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using LoadLink.LoadMatching.Persistence.Data;
using AutoMapper;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories;
using MediatR;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents;
using System.Threading;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.RabbitMQIntegrationEventManager;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using LoadLink.LoadMatching.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace ConsoleService
{
    class Program
    {
        static void Main(string[] args)
        {
           var serviceProvide= ConfigureServices();
            var loadMatchingService = serviceProvide.GetService<LoadMatchingService>();
            loadMatchingService.Start(CancellationToken.None);
               
            
        }
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var Configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
           .AddJsonFile("appsettings.json", false)
           .Build();
            // connections
            var connectionString = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            //Add EquipmentPostingContext
            services.AddDbContext<EquipmentPostingContext>(options => options.UseSqlServer(connectionString.LoadMatching));


            services.AddScoped<IConnectionFactory>(x => new ConnectionFactory(connectionString.LoadMatching));

            services.AddSingleton(Configuration);

            // mapping profiles            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //Changes in 2021-9-21

            var matchingConfig = Configuration.GetSection("MatchingConfig").Get<MatchingConfig>();
            services.AddSingleton<IMatchingConfig>(matchingConfig);

            var MqConfig = Configuration.GetSection("MqConfig").Get<MqConfig>();
            services.AddSingleton(MqConfig);

            services.AddScoped<IEquipmentPostingRepository, EquipmentPostingRepository>();

            services.AddMediatR(typeof(PostingBase).Assembly, typeof(CreatEquipmentPostingCommandHandler).Assembly, typeof(ConsoleService.LoadMatchingService).Assembly);
            services.AddScoped<IFillNotPlatformPosting, FillingNotPlatformPosting>();

           
            services.AddSingleton<IIntegrationEventHandler<PostingCreatedEvent>, PostingCreatedEventHandler>();
         

            var redisConfiguration = Configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
            services.AddSingleton<ILeadCaching, LeadCaching>();
            return services.BuildServiceProvider();
           





        }
    }
}
