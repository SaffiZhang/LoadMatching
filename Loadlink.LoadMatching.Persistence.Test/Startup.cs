using System;
using System.Collections.Generic;
using System.Text;
using Xunit.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using AutoMapper;
using LoadLink.LoadMatching.Domain.Caching;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using Microsoft.Extensions.Configuration;
using LoadLink.LoadMatching.Persistence.Data;


namespace LoadLink.LoadMatching.Persistence.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=USITDB1.linklogi.com;Database=LoadMatching_POC3;Trusted_Connection=yes;";

            services.AddSingleton<IConnectionFactory>(x => new ConnectionFactory(connectionString));
            var config = new RedisConfiguration()
            {
                Password = "L0@dL1nk#1",
                ConnectTimeout = 6000,
                AllowAdmin = true,
                Hosts = new RedisHost[] { new RedisHost() { Host = "10.50.7.236", Port = 6379 } }
            };
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(config);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILeadCaching, LeadCaching>();
            services.AddSingleton<IPersistenceRedisCacheClient, PersistenceRedisCacheClient>();
        }
    }
}
