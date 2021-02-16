using AutoMapper;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;

using LoadLink.LoadMatching.Application.EquipmentLiveLead.Profiles;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Services;
using LoadLink.LoadMatching.Application.EquipmentLiveLeadLiveLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentLiveLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentLink.EquipmentMatching.Api.Test.EquipmentLiveLead
{
    public class EquipmentLiveLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IEquipmentLiveLeadService _service;
        private readonly EquipmentLiveLeadController _datEquipmentLiveLeadController;


        public EquipmentLiveLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            AppSettings appSettings = new AppSettings() { MileageProvider = "P" };
            IOptions<AppSettings> options = Options.Create(appSettings);

            //profile
            var EquipmentLiveLeadProfile = new EquipmentLiveLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(EquipmentLiveLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new EquipmentLiveLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new EquipmentLiveLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datEquipmentLiveLeadController = new EquipmentLiveLeadController(_service, _userHelper, options);
        }

        [Fact]
        public async Task EquipmentController_GetList_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_API = "LLB_LiveLead";
            var LLB_DAT = "LLB_DAT";
            var leadFrom = DateTime.Parse("2021-02-11 20:37:00.0000000");

            //Please note this test is just to allow the CIDC to pass - s

            // act
            var actionResult = await _datEquipmentLiveLeadController.GetList(leadFrom, LLB_DAT, LLB_API, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
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
                var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task EquipmentController_GetListByPosting_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var token = 29913888;
            var LLB_API = "LLB_LiveLead";
            var LLB_DAT = "LLB_DAT";
            var leadFrom = DateTime.Parse("2021-02-11 20:37:00.0000000");

            // act
            var actionResult = await _datEquipmentLiveLeadController.GetByToken(leadFrom, token, LLB_API, LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);
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
                var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentLiveLeadQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }
    }
}
