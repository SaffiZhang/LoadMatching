using System;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace LoadLink.LoadMatching.Infrastructure.Caching
{
    public class PersistenceRedisCacheClient:IPersistenceRedisCacheClient
    {
        private IServiceProvider _serviceProvider;
        private IRedisCacheClient _redisCacheClient;

        public PersistenceRedisCacheClient(IServiceProvider serviceProvider, IRedisCacheClient redisCacheClient)
        {
            _serviceProvider = serviceProvider;
            _redisCacheClient = redisCacheClient;
        }

        public IRedisCacheClient  GetRedisCacheClient()
        {
            if (_redisCacheClient != null)
                return _redisCacheClient;
            using (var scope = _serviceProvider.CreateScope())
            {
                _redisCacheClient = scope.ServiceProvider.GetRequiredService<IRedisCacheClient>();
            }
            return _redisCacheClient;
        }
    }
}
