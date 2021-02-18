using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleSize.Profiles;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleSize;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleSize
{
    public class VehicleSizeControllerTest
    {
        private readonly IVehicleSizeService _service;
        private readonly VehicleSizeController _vehicleSizeController;
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        
        public VehicleSizeControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var vehicleSizeProfile = new VehicleSizeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleSizeProfile));
            var profile = new Mapper(configuration);

            // cacherepository
            var cacheVSize = new DatabaseFixture().MockCacheGetVehicleSizeQuery();

            // integration            
            var repository = new VehicleSizeRepository(new DatabaseFixture().ConnectionFactory);
            _service = new VehicleSizeService(repository, profile, cacheVSize.Object);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _vehicleSizeController = new VehicleSizeController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListAsync()
        {

            // act
            var actionResult = await _vehicleSizeController.GetListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleSizeQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetListByPostTypeAsync()
        {
            var postType = "E";

            // act
            var actionResult = await _vehicleSizeController.GetListByPostTypeAsync(postType);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleSizeQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
