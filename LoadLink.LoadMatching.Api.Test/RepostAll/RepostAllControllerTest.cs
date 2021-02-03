using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.RepostAll.Services;
using LoadLink.LoadMatching.Persistence.Repositories.RepostAll;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.RepostAll
{
    public class RepostAllControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IRepostAllService _service;
        private readonly RepostAllController _repostAllController;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly string apiKey = "LLB_LiveLead";

        public RepostAllControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            // integration            
            var repository = new RepostAllRepository(new DatabaseFixture().ConnectionFactory);
            _service = new RepostAllService(repository);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _repostAllController = new RepostAllController(_service, _userHelper);
        }

        [Fact]
        public async Task RepostAllAsync()
        {

            // act
            var actionResult = await _repostAllController.RepostAllAsync(apiKey);

            // assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
