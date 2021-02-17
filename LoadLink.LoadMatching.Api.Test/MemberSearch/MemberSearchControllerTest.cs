using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.MemberSearch.Profiles;
using LoadLink.LoadMatching.Application.MemberSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.MemberSearch;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.MemberSearch
{
    public class MemberSearchControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IMemberSearchService _service;
        private readonly MemberSearchController _carrierSearchController;

        public MemberSearchControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var MemberSearchProfile = new MemberSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(MemberSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new MemberSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new MemberSearchService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _carrierSearchController = new MemberSearchController(_service, _userHelper);
        }

        [Fact]
        public async Task GetMemberSearchListAsync()
        {
            // arrange
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";

            var searchRequest = new GetMemberSearchRequest
            {
                CompanyName = "Test",
                ProvSt = "",
                City = "",
                Phone = "",
                MemberType = "All",
                GetLinkUS = "N",
                ShowExcluded = 0
            };

            // act
            var actionResult = await _carrierSearchController.Search(searchRequest, LLB_EQF, LLB_TCC, LLB_TCUS);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetMemberSearchResult>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
