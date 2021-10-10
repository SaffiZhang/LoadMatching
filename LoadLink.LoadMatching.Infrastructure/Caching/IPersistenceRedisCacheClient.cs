using System;
using System.Collections.Generic;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace LoadLink.LoadMatching.Infrastructure.Caching
{
    public interface IPersistenceRedisCacheClient
    {
        IRedisCacheClient GetRedisCacheClient();
    }
}
