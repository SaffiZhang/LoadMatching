using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.NetworkMember.Profiles;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Queries;
using LoadLink.LoadMatching.Application.NetworkMembers.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.NetworkMember;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.NetworkMembers
{
    public class NetworkMembersControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly INetworkMembersService _service;
        private readonly NetworkMembersController _networkMembersController;

        public NetworkMembersControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var mappingProfile = new NetworkMembersProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappingProfile));
            var mapper = new Mapper(configuration);

            // integration            
            var repository = new NetworkMembersRepository(new DatabaseFixture().ConnectionFactory);
            _service = new NetworkMembersService(repository, mapper);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _networkMembersController = new NetworkMembersController(_service, _userHelper, mapper);
        }


        [Fact]
        public async Task Get_NetworkMembersById()
        {
            // arrange
            int id = 978;

            //act
            var actionResult = await _networkMembersController.GetAsync(id);

            //assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetNetworkMembersQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(id, model.Id);
        }


        [Fact]
        public async Task Get_NetworkMembers_List()
        {
            // act
            var actionResult = await _networkMembersController.GetListAsync(0);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetNetworkMembersQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Create_NetworkMembers()
        {
            // arrange
            var networksCommand = new CreateNetworkMembersCommand
            {
               NetworkId = 1835,
               MemberCustCD = "AB-TRTE"
            };

            // act
            var actionResult = await _networkMembersController.CreateAsync(networksCommand);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

   
        [Fact]
        public async Task Delete_NetworkMembers()
        {
            // arrange
            int networksId = 1834;
            string custCd = "CHALNGE";

            // act
            var actionResult = await _networkMembersController.DeleteAsync(networksId,custCd);

            // assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
