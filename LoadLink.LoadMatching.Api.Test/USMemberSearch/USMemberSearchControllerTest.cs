using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USMemberSearch.Profiles;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.USMemberSearch;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.USMemberSearch
{
    public class USCarrierSearchControllerTest
    {
        private readonly IUSMemberSearchService _service;
        private readonly USMemberSearchController _USMemberSearchController;
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly string apiKey = "LLB_LiveLead";

        public USCarrierSearchControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var USMemberSearchProfile = new USMemberSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(USMemberSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USMemberSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USMemberSearchService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _USMemberSearchController = new USMemberSearchController(_service, _userHelper);
        }

        [Fact]
        public async Task GetUSMemberSearchListAsync()
        {
            // arrange
            var searchRequest = new GetUSMemberSearchCommand
            {
                CustCd = "TCORELL",
                CompanyName = "",
                Phone = "",
                ProvSt = "",
                ShowExcluded = SearchType.All
            };

            // act
            var actionResult = await _USMemberSearchController.GetUSMemberSearchAsync(searchRequest, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetUSMemberSearchQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
