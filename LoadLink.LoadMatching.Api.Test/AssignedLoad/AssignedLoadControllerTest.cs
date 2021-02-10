using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries;
using LoadLink.LoadMatching.Application.AssignedLoad.Profiles;
using LoadLink.LoadMatching.Application.AssignedLoad.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.AssignedLoad;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.AssignedLoad
{
    public class AssignedLoadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IAssignedLoadService _service;
        private readonly AssignedLoadController _assignedLoadController;
        private readonly IUserSubscriptionService _userSubscriptionService;

        public AssignedLoadControllerTest()
        {
            var userId = 34318;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            var assignedLoadProfile = new AssignedLoadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(assignedLoadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new AssignedLoadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new AssignedLoadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _assignedLoadController = new AssignedLoadController(_service, _userHelper);
        }

        [Fact]
        public async Task Get_AssignedLoad()
        {
            //arrange
            int token = 9308505;

            // act
            var actionResult = await _assignedLoadController.GetAsync(token);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<GetAssignedLoadQuery>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Get_AssignedLoad_List()
        {
            // act
            var actionResult = await _assignedLoadController.GetListAsync();
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetAssignedLoadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Create_AssignedLoad()
        {
            // arrange
            var createCmd = new CreateAssignedLoadCommand
            {
                Token = 9309171,
                Instruction = "Test Create2"
            };

            // act
            var actionResult = await _assignedLoadController.CreateAsync(createCmd);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
                Assert.IsType<BadRequestObjectResult>(actionResult);
            else
                Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task Update_CustomerTracking()
        {
            // arrange
            string APIKey = "LLB_LiveLead";
            var updateCmd = new UpdateCustomerTrackingCommand
            {
                ID = 1565,
                UserId = 0,
                CustTracking = true
            };

            // act
            var actionResult = await _assignedLoadController.UpdateCustomerTrackingAsync(updateCmd, APIKey);

            // assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task Delete_AssignedLoad()
        {
            //arrange
            int token = 9308535;

            // act
            var actionResult = await _assignedLoadController.DeleteAsync(token);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<DeleteAssignedLoadQuery>(viewResult.Value);
                Assert.NotNull(model);
            }
        }
    }
}
