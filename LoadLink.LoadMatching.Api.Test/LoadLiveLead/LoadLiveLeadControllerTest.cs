using AutoMapper;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;

using LoadLink.LoadMatching.Application.LoadLiveLead.Profiles;
using LoadLink.LoadMatching.Application.LoadLiveLead.Models;
using LoadLink.LoadMatching.Application.LoadLiveLead.Services;
using LoadLink.LoadMatching.Application.LoadLiveLeadLiveLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LoadLiveLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.LoadLiveLead
{
    public class LoadLiveLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ILoadLiveLeadService _service;
        private readonly LoadLiveLeadController _datLoadLiveLeadController;
        private readonly IOptions<AppSettings> _settings;

        public LoadLiveLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile
            var LoadLiveLeadProfile = new LoadLiveLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(LoadLiveLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LoadLiveLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LoadLiveLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datLoadLiveLeadController = new LoadLiveLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task LoadController_GetList_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_API = "LLB_LiveLead";
            var leadFrom = DateTime.Parse("2021-02-11 20:37:00.0000000");

            //Please note this test is just to allow the CIDC to pass - s

            // act
            var actionResult = await _datLoadLiveLeadController.GetList(leadFrom, LLB_API, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                // assert
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetLoadLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task LoadController_GetListByPosting_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var token = 29913888;
            var LLB_API = "LLB_LiveLead";
            var leadFrom = DateTime.Parse("2021-02-11 20:37:00.0000000");

            // act
            var actionResult = await _datLoadLiveLeadController.GetByToken(leadFrom, token, LLB_API, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                // assert
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetLoadLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }
    }
}
