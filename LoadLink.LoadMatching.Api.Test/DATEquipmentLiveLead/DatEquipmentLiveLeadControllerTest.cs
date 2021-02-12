using AutoMapper;
using EquipmentLink.EquipmentMatching.Application.DATEquipmentLiveLead.Profiles;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentLink.EquipmentMatching.Api.Test.DATEquipmentLiveLead
{
    public class DATEquipmentLiveLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IDatEquipmentLiveLeadService _service;
        private readonly DatEquipmentLiveLeadController _datEquipmentLiveLeadController;


        public DATEquipmentLiveLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            AppSettings appSettings = new AppSettings() { MileageProvider = "P" };
            IOptions<AppSettings> options = Options.Create(appSettings);

            //profile
            var DATEquipmentLiveLeadProfile = new DatEquipmentLiveLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(DATEquipmentLiveLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new DatEquipmentLiveLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new DatEquipmentLiveLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datEquipmentLiveLeadController = new DatEquipmentLiveLeadController(_service, _userHelper, options);
        }

        [Fact]
        public async Task DatEquipmentController_GetList_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_API = "LLB_LiveLead";
            var LLB_DAT = "LLB_DAT";
            var leadFrom = DateTime.Parse("2021-02-11 20:37:00.0000000");

            // act
            var actionResult = await _datEquipmentLiveLeadController.GetList(leadFrom, LLB_DAT, LLB_API, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatEquipmentLiveLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task DatEquipmentController_GetListByPosting_Success()
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

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatEquipmentLiveLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
