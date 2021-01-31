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
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Infrastructure.Caching
{
    public class CacheRepository<T> : ICacheRepository<T> where T : class
    {

        private readonly IMemoryCache _cache;

        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CacheRepository(IMemoryCache cache, MemoryCacheEntryOptions cacheOptions)
        {
            _cache = cache;
            _cacheOptions = cacheOptions;
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
            if (_cache.TryGetValue(keyname, out T returnData))
                return returnData;

            returnData = await callback();

            _cache.Set(keyname, returnData, _cacheOptions);

            return returnData;
        }

        private async Task<IList<T>> TryGetAndSet(string keyname, Func<Task<List<T>>> callback)
        {
            if (_cache.TryGetValue(keyname, out IList<T> returnData))
                return returnData;

            returnData = await callback();

            _cache.Set(keyname, returnData, _cacheOptions);

            return returnData;
        }
    }
}
