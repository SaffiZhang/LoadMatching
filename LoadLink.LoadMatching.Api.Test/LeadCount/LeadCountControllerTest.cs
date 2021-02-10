using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LeadCount.Models.Queries;
using LoadLink.LoadMatching.Application.LeadCount.Profiles;
using LoadLink.LoadMatching.Application.LeadCount.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LeadCount;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.LeadCount
{
    public class LeadCountControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ILeadsCountService _service;
        private readonly LeadCountController _leadCountController;

        public LeadCountControllerTest()
        {
            var userId = 34344;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            //profile
            var LeadsCountProfile = new LeadsCountProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(LeadsCountProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LeadsCountRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LeadsCountService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _leadCountController = new LeadCountController(_service, _userHelper);
        }

        [Fact]
        public async Task GetAsync()
        {
            // arrange
            var LLB_DAT = "LLC_DAT";
            var type = "L";
            var token = 9309169;

            // act
            var actionResult = await _leadCountController.GetLeadsCountAsync(token, type, LLB_DAT);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLeadsCountQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
