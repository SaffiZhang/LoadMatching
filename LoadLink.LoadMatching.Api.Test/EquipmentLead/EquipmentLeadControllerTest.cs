
using AutoMapper;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.EquipmentLead.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentLead.Profiles;
using LoadLink.LoadMatching.Application.EquipmentLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentLead;

namespace LoadLink.LoadMatching.Api.Test.EquipmentLead
{
    public class EquipmentLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IEquipmentLeadService _service;
        private readonly EquipmentLeadController _equipmentLeadController;
        private readonly IOptions<AppSettings> _settings;

        public EquipmentLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile
            var equipmentLeadProfile = new EquipmentLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(equipmentLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new EquipmentLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new EquipmentLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _equipmentLeadController = new EquipmentLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task GetListAsync()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";

            // act
            var actionResult = await _equipmentLeadController.GetListAsync(LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetByPostingAsync()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var token = 29913722;

            // act
            var actionResult = await _equipmentLeadController.GetByPostingAsync(token,LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetCombinedAsync()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_DAT = "LLC_DAT";
            var token = 29913722;

            // act
            var actionResult = await _equipmentLeadController.GetCombinedAsync(token, LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentLeadCombinedQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
