using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using LoadLink.LoadMatching.Application.RIRate.Models.Queries;
using LoadLink.LoadMatching.Application.RIRate.Profiles;
using LoadLink.LoadMatching.Application.RIRate.Services;
using LoadLink.LoadMatching.Persistence.Repositories.RIRate;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.RIRate
{
    public class RIRateControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IRIRateService _service;
        private readonly RIRateController _RIRateController;
        private readonly string apiKey = "LLB_LiveLead";

        public RIRateControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var RIRateProfile = new RIRateProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(RIRateProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new RIRateRepository(new DatabaseFixture().ConnectionFactory);
            _service = new RIRateService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _RIRateController = new RIRateController(_service, _userHelper);
        }

        [Fact]
        public async Task GetRIRateListAsync()
        {
            // arrange
            var searchRequest = new GetRIRateCommand
            {
                VehicleType = "R",
                SrceSt = "QC",
                SrceCity = "Montreal",
                DestSt = "ON",
                DestCity = "Toronto"
            };

            // act
            var actionResult = await _RIRateController.GetRIRateAsync(searchRequest, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetRIRateQuery>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
