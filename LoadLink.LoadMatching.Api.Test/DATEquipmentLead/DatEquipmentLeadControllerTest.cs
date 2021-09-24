using AutoMapper;
using EquipmentLink.EquipmentMatchings.Application.DATEquipmentLead.Profiles;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead;

namespace EquipmentLink.EquipmentMatchings.Api.Test.DATEquipmentLead
{
    public class DatEquipmentLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IDatEquipmentLeadService _service;
        private readonly DatEquipmentLeadController _datEquipmentLeadController;
        private readonly IOptions<AppSettings> _settings;

        public DatEquipmentLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile
            var datEquipmentLeadProfile = new DatEquipmentLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(datEquipmentLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new DatEquipmentLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new DatEquipmentLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datEquipmentLeadController = new DatEquipmentLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task DatEquipmentController_GetList_Success()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_DAT = "LLB_DAT";

            // act
            var actionResult = await _datEquipmentLeadController.GetListAsync(LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatEquipmentLeadQuery>>(viewResult.Value);
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
            var LLB_DAT = "LLB_DAT";
            var token = 29913888;

            // act
            var actionResult = await _datEquipmentLeadController.GetAsync(token, LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatEquipmentLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
