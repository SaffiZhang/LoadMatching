// ***********************************************************************
// Assembly         : LoadLink.LoadMatching.Infrastructure
// Author           : jbuenaventura
// Created          : 2021-01-02
//
// Last Modified By : jbuenaventura
// Last Modified On : 2021-01-02
// ***********************************************************************
// <copyright file="CachRepository.cs" company="LoadLink.LoadMatching.Infrastructure">
//     Copyright (c) LoadLink Technologies. All rights reserved.
// </copyright>
// <summary>Caching of data</summary>
// ***********************************************************************
using LoadLink.LoadMatching.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Infrastructure.Caching
{
    public class CacheRepository<T> : ICacheRepository<T> where T : class
    {
        private readonly IDistributedCache _cache;

        private readonly DistributedCacheEntryOptions _cacheOptions;

        public CacheRepository(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _cacheOptions = new DistributedCacheEntryOptions();

            var cacheSetting = configuration.GetSection("ServiceCacheSettings");

            // five minutes default
            var slidingExpiration = Convert.ToInt32(cacheSetting["DefaultCacheSetting:SlidingExpiration"] ?? "5");
            _cacheOptions.SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration);

            // clear the cache at midnight so we get fresh data
            var expireTimespan = new DateTimeOffset(DateTime.Today.AddHours(24));
            _cacheOptions.AbsoluteExpiration = expireTimespan;
        }

        public async Task<IList<T>> GetMany(string keyName, Expression<Func<T, bool>> expression, Func<Task<List<T>>> callback)
        {
            IList<T> t;

            t = await TryGetAndSet(keyName, callback);

            return t.AsQueryable().Where(expression).ToList();
        }

        public async Task<IList<T>> GetAll(string keyName, Func<Task<List<T>>> callback)
        {
            return await TryGetAndSet(keyName, callback);
        }

        public async Task<T> GetSingle(string keyName, Func<Task<T>> callback)
        {
            return await TryGetAndSetSingle(keyName, callback);
        }

        private async Task<T> TryGetAndSetSingle(string keyname, Func<Task<T>> callback)
        {
            var result = await _cache.GetAsync(keyname);
            if (result != null)
            {
                var data = Encoding.UTF8.GetString(result);
                return JsonConvert.DeserializeObject<T>(data);
            }

            var returnData = await callback();
            if (returnData == null)
                return null;

            var serializeData = JsonConvert.SerializeObject(returnData);

            _cache.Set(keyname, Encoding.UTF8.GetBytes(serializeData), _cacheOptions);

            return returnData;
        }

        private async Task<IList<T>> TryGetAndSet(string keyname, Func<Task<List<T>>> callback)
        {
            var result = await _cache.GetAsync(keyname);
            if (result != null)
            {
                var data = Encoding.UTF8.GetString(result);
                return JsonConvert.DeserializeObject<IList<T>>(data);
            }

            var returnData = await callback();
            if (returnData == null)
                return null;

            var serializeData = JsonConvert.SerializeObject(returnData);
            _cache.Set(keyname, Encoding.UTF8.GetBytes(serializeData), _cacheOptions);

            return returnData;
        }
    }
}
