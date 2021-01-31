// ***********************************************************************
// Assembly         : LoadLink.LoadMatching.Application
// Author           : jbuenaventura
// Created          : 2021-01-02
//
// Last Modified By : jbuenaventura
// Last Modified On : 2021-01-02
// ***********************************************************************
// <copyright file="UserSubscriptionService.cs" company="LoadLink.LoadMatching.Application">
//     Copyright (c) LoadLink Technologies. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LoadLink.LoadMatching.Application.Caching;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.UserSubscription.Services
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly ICacheRepository<UserApiKeyQuery> _userApiKeyCache;

        public UserSubscriptionService(ICacheRepository<UserApiKeyQuery> userApiKeyCache, IUserSubscriptionRepository userSubscriptionRepository)
        {
            _userApiKeyCache = userApiKeyCache;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        /// <summary>
        /// Get api keys from cache if available. Otherwise gets data from repository then store it in cache.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserApiKeyQuery> GetUserApiKeys(int userId)
        {
            var result = await _userApiKeyCache.GetSingle($"UserId-{userId}", async () => {
                var userApiKey = await _userSubscriptionRepository.UserApiKeys(userId);
                return new UserApiKeyQuery { UserId = userId, ApiKeys = userApiKey?.ApiKeys };
            });

            return result;
        }

        public async Task<bool> IsValidApiKey(int userId, string apiKey)
        {
            var userApiKeys = await GetUserApiKeys(userId);
            if (userApiKeys == null)
                return false;

            if (userApiKeys.ApiKeys.Any(x => x.Equals(apiKey, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }
    }
}
