using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USCarrierSearch.Profiles;
using LoadLink.LoadMatching.Application.USCarrierSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.USCarrierSearch;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.USCarrierSearch
{
    public class USCarrierSearchControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IUSCarrierSearchService _service;
        private readonly USCarrierSearchController _USCarrierSearchController;
        
        public USCarrierSearchControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var USCarrierSearchProfile = new USCarrierSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(USCarrierSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USCarrierSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USCarrierSearchService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _USCarrierSearchController = new USCarrierSearchController(_service, _userHelper);
        }

        [Fact]
        public async Task GetUSCarrierSearchListAsync()
        {
            // arrange
            var apiKey = "LLB_LiveLead";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";

            var searchRequest = new GetUSCarrierSearchCommand
            {
                UserId = 1235,
                SrceSt = "",
                SrceCity = "",
                SrceRadius = 0,
                DestSt = "",
                DestCity = "",
                DestRadius = 0,
                VehicleType = "V",
                VehicleSize = "U",
                CompanyName = "",
                GetMexico = "Y"
            };

            // act
            var actionResult = await _USCarrierSearchController.GetUSCarrierSearchAsync(searchRequest, apiKey, LLB_EQF, LLB_TCC, LLB_TCUS);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetUSCarrierSearchQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
