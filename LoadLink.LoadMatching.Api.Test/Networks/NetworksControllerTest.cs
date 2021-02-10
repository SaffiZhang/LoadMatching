using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Models.Queries;
using LoadLink.LoadMatching.Application.Networks.Profiles;
using LoadLink.LoadMatching.Application.Networks.Services;
using LoadLink.LoadMatching.Persistence.Repositories.Networks;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.JsonPatch;

namespace LoadLink.LoadMatching.Api.Test.Networks
{
    public class NetworksControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly INetworksService _service;
        private readonly NetworksController _networksController;

        public NetworksControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var mappingProfile = new NetworksProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappingProfile));
            var mapper = new Mapper(configuration);

            // integration            
            var repository = new NetworksRepository(new DatabaseFixture().ConnectionFactory);
            _service = new NetworksService(repository, mapper);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _networksController = new NetworksController(_service, _userHelper, mapper);
        }

        [Fact]
        public async Task Get_Networks()
        {
            // arrange
            int networksId = 1878;

            //act
            var actionResult = await _networksController.GetAsync(networksId);

            //assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetNetworksQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(networksId, model.Id);
        }

        [Fact]
        public async Task Get_Networks_List()
        {
            // act
            var actionResult = await _networksController.GetListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetNetworksQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Create_Networks()
        {
            // arrange
            var networksCommand = new NetworksCommand
            {
                UserId = 34351,
                CustCD = "TCORELL",
                Name = "MyNetwork",
                Type = "Private"
            };

            // act
            var actionResult = await _networksController.CreateAsync(networksCommand);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Update_Template_Posting()
        {
            // arrange
            var networksId = 1878;
            var patchNetworksCommand = new PatchNetworksCommand
            {
                Name = "MyNetworkUpdate"
            };

            // act
            var actionResult = await _networksController.UpdateAsync(networksId, patchNetworksCommand);

            // assert  
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task Patch_Template_Posting()
        {
            // arrange
            var patchDocument = new JsonPatchDocument<PatchNetworksCommand>();
            var networksId = 1878;
            var patchNetworksCommand = new PatchNetworksCommand
            {
                Name = "MyNetworkPatch"
            };

            patchDocument.Add(x => x, patchNetworksCommand);

            // act
            var actionResult = await _networksController.PatchAsync(networksId, patchDocument);

            // assert  
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task Delete_Networks()
        {
            // arrange
            short networksId = 1000;

            // act
            var actionResult = await _networksController.DeleteAsync(networksId);

            // assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
