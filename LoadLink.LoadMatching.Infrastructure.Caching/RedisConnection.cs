using System;
using StackExchange.Redis;

namespace LoadLink.LoadMatching.Caching
{
    public class RedisConnection
    {
       
        private ConnectionMultiplexer _redis;

        public RedisConnection(RedisConfiguration redisConfiguration)
        {

            _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { redisConfiguration.Server } , Password = redisConfiguration.Password  });
        }
        public ConnectionMultiplexer Redis { get { return _redis; } }
    }
}
