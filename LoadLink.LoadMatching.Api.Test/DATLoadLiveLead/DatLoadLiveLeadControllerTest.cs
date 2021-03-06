using AutoMapper;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Profiles;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Repositories.DATLoadLead;

namespace LoadLink.LoadMatching.Api.Test.DATLoadLiveLead
{
    public class DATLoadLiveLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IDatLoadLiveLeadService _service;
        private readonly DatLoadLiveLeadController _datLoadLiveLeadController;
        private readonly IOptions<AppSettings> _settings;

        public DATLoadLiveLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile

            var DATLoadLiveLeadProfile = new DatLoadLiveLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(DATLoadLiveLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new DatLoadLiveLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new DatLoadLiveLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datLoadLiveLeadController = new DatLoadLiveLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task DatLoadController_GetList_Success()
        {
            // arrange
            var LLB_QP = "LLC_QP";
            var LLB_EQF = "LLC_EQF";
            var LLB_TCC = "LLC_TCC";
            var LLB_TCUS = "LLC_TCUS";
            var LLB_API = "LLC_LiveLead";
            var LLB_DAT = "LLC_DAT";
            var leadFrom = DateTime.Parse("2021-02-12 13:34:00.0000000");

            // act
            var actionResult = await _datLoadLiveLeadController.GetList(leadFrom, LLB_DAT, LLB_API, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
            var okResult = actionResult as OkObjectResult;


            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                // assert
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetDatLoadLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task DatLoadController_GetListByPosting_Success()
        {
            // arrange
            var LLB_QP = "LLC_QP";
            var LLB_EQF = "LLC_EQF";
            var LLB_TCC = "LLC_TCC";
            var LLB_TCUS = "LLC_TCUS";
            var token = 29913888;
            var LLB_API = "LLC_LiveLead";
            var LLB_DAT = "LLC_DAT";
            var leadFrom = DateTime.Parse("2021-02-12 13:33:00.0000000");

            // act
            var actionResult = await _datLoadLiveLeadController.GetByToken(leadFrom, token, LLB_API, LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
            var okResult = actionResult as OkObjectResult;


            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                // assert
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetDatLoadLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }
    }
}
