using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LoadLead.Models.Queries;
using LoadLink.LoadMatching.Application.LoadLead.Profiles;
using LoadLink.LoadMatching.Application.LoadLead.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LoadLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.LoadLead
{
    public class LoadLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ILoadLeadService _service;
        private readonly LoadLeadController _loadLeadController;

        public LoadLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var LoadLeadProfile = new LoadLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(LoadLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LoadLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LoadLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _loadLeadController = new LoadLeadController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListsAsync()
        {
            // arrange
            var QPAPIKey = "LLB_QP";
            var EQFAPIKey = "LLB_EQF";
            var TCUSAPIKey = "LLB_TCUS";
            var TCCAPIKey = "LLB_TCC";

            // act
            var actionResult = await _loadLeadController.GetListsAsync(QPAPIKey, EQFAPIKey, TCUSAPIKey, TCCAPIKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLoadLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetByPostingAsync()
        {
            // arrange
            var token = 9309171;
            var QPAPIKey = "LLB_QP";
            var EQFAPIKey = "LLB_EQF";
            var TCUSAPIKey = "LLB_TCUS";
            var TCCAPIKey = "LLB_TCC";

            // act
            var actionResult = await _loadLeadController.GetByPostingAsync(token, QPAPIKey, EQFAPIKey, TCUSAPIKey, TCCAPIKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLoadLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetCombinedAsync()
        {
            // arrange
            var token = 9309171;
            var DATAPIKey = "LLB_DAT";
            var QPAPIKey = "LLB_QP";
            var EQFAPIKey = "LLB_EQF";
            var TCUSAPIKey = "LLB_TCUS";
            var TCCAPIKey = "LLB_TCC";

            // act
            var actionResult = await _loadLeadController.GetCombinedAsync(token, DATAPIKey, QPAPIKey, EQFAPIKey, TCUSAPIKey, TCCAPIKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLoadLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
