using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using LoadLink.LoadMatching.Application.PDRatio.Models.Queries;
using LoadLink.LoadMatching.Application.PDRatio.Profiles;
using LoadLink.LoadMatching.Application.PDRatio.Services;
using LoadLink.LoadMatching.Persistence.Repositories.PDRatio;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.PDRatio
{
    public class PDRatioControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IPDRatioService _service;
        private readonly PDRatioController _pdRatioController;
        private readonly string apiKey = "LLB_LiveLead";

        public PDRatioControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var pdRatioProfile = new PDRatioProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(pdRatioProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new PDRatioRepository(new DatabaseFixture().ConnectionFactory);
            _service = new PDRatioService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _pdRatioController = new PDRatioController(_service, _userHelper);
        }

        [Fact]
        public async Task GetPDRatioListAsync()
        {
            // arrange
            var searchRequest = new GetPDRatioCommand
            {
                VehicleType = "R",
                SrceSt = "QC",
                SrceCity = "Montreal",
                DestSt = "ON",
                DestCity = "Toronto"
            };

            // act
            var actionResult = await _pdRatioController.GetPDRatioAsync(searchRequest, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetPDRatioQuery>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
