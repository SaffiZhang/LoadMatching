using System;
using System.Collections.Generic;
using System.Text;
using Xunit.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;


namespace LoadLink.LoadMatching.Persistence.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new RedisConfiguration()
            {
                Password = "L0@dL1nk#1",
                ConnectTimeout = 6000,
                AllowAdmin = true,
                Hosts = new RedisHost[] { new RedisHost() { Host = "10.50.7.236", Port = 6379 } }
            };
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(config);
        }
    }
}
