using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.CarrierSearch.Profiles;
using LoadLink.LoadMatching.Application.CarrierSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.CarrierSearch;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.CarrierSearch
{
    public class CarrierSearchControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ICarrierSearchService _service;
        private readonly CarrierSearchController _carrierSearchController;

        public CarrierSearchControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var CarrierSearchProfile = new CarrierSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(CarrierSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new CarrierSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new CarrierSearchService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _carrierSearchController = new CarrierSearchController(_service, _userHelper);
        }

        [Fact]
        public async Task GetCarrierSearchListAsync()
        {
            // arrange
            var apiKey = "LLB_CarrSearch";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";

            var searchRequest = new GetCarrierSearchRequest
            {
                SrceSt = "",
                SrceCity = "",
                SrceRadius = 0,
                DestSt = "",
                DestCity = "",
                DestRadius = 0,
                VehicleType = "",
                VehicleSize = "",
                PostingAttrib ="",
                CompanyName = "TRANS",
                GetDat = "Y",
                GetMexico = "N"
            };

            // act
            var actionResult = await _carrierSearchController.Search(searchRequest, apiKey, LLB_EQF, LLB_TCC, LLB_TCUS);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetCarrierSearchResult>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
