using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LoadPosition.Models.Queries;
using LoadLink.LoadMatching.Application.LoadPosition.Profiles;
using LoadLink.LoadMatching.Application.LoadPosition.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LoadPosition;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.LoadPosition
{
    public class LoadPositionControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly ILoadPositionService _service;
        private readonly LoadPositionController _loadPositionController;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public LoadPositionControllerTest()
        {
            var userId = 34318;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            var loadPositionProfile = new LoadPositionProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(loadPositionProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LoadPositionRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LoadPositionService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _loadPositionController = new LoadPositionController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListAsync()
        {
            //arrange
            var APIkey = "LLB_DAT";
            int token = 9308505;

            // act
            var actionResult = await _loadPositionController.GetAsync(token, APIkey);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetLoadPositionQuery>>(viewResult.Value);
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
            var actionResult = await _loadPositionController.CreateAsync(token, APIkey);

            Assert.IsType<OkResult>(actionResult);
        }
    }
}
