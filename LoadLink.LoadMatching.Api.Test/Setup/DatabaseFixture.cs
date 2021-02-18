using AutoMapper;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using Microsoft.Extensions.Options;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using System.Collections.Generic;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;

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

        // VehicleSize
        public Mock<CacheRepository<IEnumerable<GetVehicleSizeQuery>>> MockCacheGetVehicleSizeQuery()
        {
            var memOptions = new MemoryCacheOptions();
            var expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expireTimespan };

            var memCache = new MemoryCache(memOptions);
            memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(2));

            return new Mock<CacheRepository<IEnumerable<GetVehicleSizeQuery>>>(memCache, memoryCacheEntryOptions);
        }

        // VehicleType
        public Mock<CacheRepository<IEnumerable<GetVehicleTypesQuery>>> MockCacheGetVehicleTypesQuery()
        {
            var memOptions = new MemoryCacheOptions();
            var expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expireTimespan };

            var memCache = new MemoryCache(memOptions);
            memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(2));

            return new Mock<CacheRepository<IEnumerable<GetVehicleTypesQuery>>>(memCache, memoryCacheEntryOptions);
        }

        // VehicleAttributes
        public Mock<CacheRepository<IEnumerable<GetVehicleAttributeQuery>>> MockCacheGetVehicleAttributeQuery()
        {
            var memOptions = new MemoryCacheOptions();
            var expireTimespan = new TimeSpan((DateTime.Now - DateTime.Today).Ticks);
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expireTimespan };

            var memCache = new MemoryCache(memOptions);
            memoryCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(2));

            return new Mock<CacheRepository<IEnumerable<GetVehicleAttributeQuery>>>(memCache, memoryCacheEntryOptions);
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
            });

            return new Mapper(configuration);
        }

        public IOptions<AppSettings> AppSettings()
        {
            var appSettings = new AppSettings();

            appSettings.LeadsCap = 500;
            appSettings.MileageProvider = "P";

            var options = Options.Create(appSettings);
            return options;
        }
    }
}
