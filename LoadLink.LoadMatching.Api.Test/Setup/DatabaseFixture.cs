using AutoMapper;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;

namespace LoadLink.LoadMatching.Api.Test.Setup
{
    public class DatabaseFixture
    {
        public const string ConnectionString = "Server=udvlpdb01.linklogi.com;Database=LoadMatching;Trusted_Connection=yes;";

        public DatabaseFixture()
        {

        }

        /// <summary>
        /// Integration test setup
        /// </summary>
        public IConnectionFactory ConnectionFactory => new ConnectionFactory(ConnectionString);

        public Mock<IUserSubscriptionRepository> UserSubscriptionRepository => new Mock<IUserSubscriptionRepository>();


        public Mock<CacheRepository<UserApiKeyQuery>> MockCacheUserApiKey()
        {
            var memOptions = new MemoryCacheOptions();
            var expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expireTimespan };

            var memCache = new MemoryCache(memOptions);
            memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(2));

            return new Mock<CacheRepository<UserApiKeyQuery>>(memCache, memoryCacheEntryOptions);
        }

        // specific mapping configuration
        public Mapper GetConfigProfile(Profile profile)
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(profile));
            return new Mapper(configuration);
        }

        // all mapping configuration
        public Mapper ConfigProfiles()
        {

            var configuration = new MapperConfiguration(config =>
            {
                //config.AddProfile(new AccountProfile());
                // add the rest of the mapping profiles here ... 
            });

            return new Mapper(configuration);
        }
    }
}
