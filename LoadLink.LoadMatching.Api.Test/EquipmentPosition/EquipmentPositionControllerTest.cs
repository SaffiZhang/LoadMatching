
using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.EquipmentPosition.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentPosition.Profiles;
using LoadLink.LoadMatching.Application.EquipmentPosition.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentPosition;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.EquipmentPosition
{
    public class EquipmentPositionControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IEquipmentPositionService _service;
        private readonly EquipmentPositionController _equipmentPositionController;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public EquipmentPositionControllerTest()
        {
            var userId = 34318;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            var equipmentPositionProfile = new EquipmentPositionProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(equipmentPositionProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new EquipmentPositionRepository(new DatabaseFixture().ConnectionFactory);
            _service = new EquipmentPositionService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _equipmentPositionController = new EquipmentPositionController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListAsync()
        {
            //arrange
            var APIkey = "LLB_DAT";
            int token = 9308505;

            // act
            var actionResult = await _equipmentPositionController.GetAsync(token, APIkey);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentPositionQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Create_AssignedLoad()
        {
            // arrange
            var APIkey = "LLB_DAT";
            int token = 9308505;

            // act
            var actionResult = await _equipmentPositionController.CreateAsync(token, APIkey);

            Assert.IsType<OkResult>(actionResult);
        }
    }
}
