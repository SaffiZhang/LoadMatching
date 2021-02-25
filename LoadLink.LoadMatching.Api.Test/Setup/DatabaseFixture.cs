using AutoMapper;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Infrastructure.Caching;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Microsoft.Extensions.Options;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using System.Collections.Generic;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Api.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

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

        // ApiKeys
        public Mock<CacheRepository<UserApiKeyQuery>> MockCacheUserApiKey()
        {
            return new Mock<CacheRepository<UserApiKeyQuery>>(GetDistributedCache(), GetConfiguration());
        }

        // VehicleSize
        public Mock<CacheRepository<IEnumerable<GetVehicleSizeQuery>>> MockCacheGetVehicleSizeQuery()
        {
            return new Mock<CacheRepository<IEnumerable<GetVehicleSizeQuery>>>(GetDistributedCache(), GetConfiguration());
        }

        // VehicleType
        public Mock<CacheRepository<IEnumerable<GetVehicleTypesQuery>>> MockCacheGetVehicleTypesQuery()
        {
            return new Mock<CacheRepository<IEnumerable<GetVehicleTypesQuery>>>(GetDistributedCache(), GetConfiguration());
        }

        // VehicleAttributes
        public Mock<CacheRepository<IEnumerable<GetVehicleAttributeQuery>>> MockCacheGetVehicleAttributeQuery()
        {
            return new Mock<CacheRepository<IEnumerable<GetVehicleAttributeQuery>>>(GetDistributedCache(), GetConfiguration());
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
            var appSetting = new AppSetting();

            appSetting.LeadsCap = 500;
            appSetting.MileageProvider = "P";

            appSettings.AppSetting = appSetting;

            var options = Options.Create(appSettings);
            return options;
        }

        public IDistributedCache GetDistributedCache()
        {
            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            var distributedCache = new MemoryDistributedCache(opts);
            return distributedCache;
        }

        public IConfiguration GetConfiguration()
        {
            var memorySettings = new Dictionary<string, string>
            {
                { "ServiceCacheSettings", "DefaultCacheSetting"}
            };
            var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(memorySettings)
            .Build();

            return configuration;
        }

    }
}
