using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.DATLoadLead.Models.Queries;
using LoadLink.LoadMatching.Application.DATLoadLead.Profiles;
using LoadLink.LoadMatching.Application.DATLoadLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Persistence.Repositories.DatLoadLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.DATLoadLead
{
    public class DatLoadLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IDatLoadLeadService _service;
        private readonly DatLoadLeadController _datLoadLeadController;
        private readonly IOptions<AppSettings> _settings;

        public DatLoadLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile
            var datLoadLeadProfile = new DatLoadLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(datLoadLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new DatLoadLeadRepository(new DatabaseFixture().ConnectionFactory, _settings);
            _service = new DatLoadLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datLoadLeadController = new DatLoadLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task GetListAsync()
        {
            // arrange
            var LLB_QP = "LLB_QP";
            var LLB_EQF = "LLB_EQF";
            var LLB_TCC = "LLB_TCC";
            var LLB_TCUS = "LLB_TCUS";
            var LLB_DAT = "LLB_DAT";

            // act
            var actionResult = await _datLoadLeadController.GetListAsync(LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatLoadLeadQuery>>(viewResult.Value);
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
            var LLB_DAT = "LLB_DAT";
            var token = 9309180;

            // act
            var actionResult = await _datLoadLeadController.GetByPostingAsync(token, LLB_DAT, LLB_QP, LLB_EQF, LLB_TCUS, LLB_TCC);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetDatLoadLeadQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
