using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleAttribute.Profiles;
using LoadLink.LoadMatching.Application.VehicleAttribute.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleAttribute;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleAttribute
{
    public class USMemberSearchControllerTest
    {
        private readonly IUSMemberSearchService _service;
        private readonly VehicleAttributeController _vehicleAttributeController;
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public USMemberSearchControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var vehicleAttributeProfile = new USMemberSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleAttributeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USMemberSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USMemberSearchService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _vehicleAttributeController = new VehicleAttributeController(_service, _userHelper);
        }

        [Fact]
        public async Task GetVehicleAttributeListAsync()
        {

            // act
            var actionResult = await _vehicleAttributeController.GetListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleAttributeQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
