using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Profiles;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleType;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleType
{
    public class VehicleSizeControllerTest
    {
        private readonly IVehicleTypeService _service;
        private readonly VehicleTypeController _vehicleTypeController;
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public VehicleSizeControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var vehicleTypeProfile = new VehicleTypeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleTypeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new VehicleTypeRepository(new DatabaseFixture().ConnectionFactory);
            _service = new VehicleTypeService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _vehicleTypeController = new VehicleTypeController(_service, _userHelper);
        }

        [Fact]
        public async Task GetVehicleTypeListAsync()
        {

            // act
            var actionResult = await _vehicleTypeController.GetListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleTypesQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
