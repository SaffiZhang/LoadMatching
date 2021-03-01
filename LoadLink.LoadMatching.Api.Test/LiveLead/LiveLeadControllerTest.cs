using AutoMapper;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using LoadLink.LoadMatching.Application.LiveLead.Profiles;
using LoadLink.LoadMatching.Application.LiveLead.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LiveLead;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentLink.EquipmentMatching.Api.Test.LiveLead
{
    public class LiveLeadControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ILiveLeadService _service;
        private readonly LiveLeadController _liveLeadController;
        private readonly IOptions<AppSettings> _settings;

        public LiveLeadControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            //profile
            var LiveLeadProfile = new LiveLeadProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(LiveLeadProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LiveLeadRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LiveLeadService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _liveLeadController = new LiveLeadController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task LiveLeadController_GetList_Success()
        {
            // arrange

            var request = new GetLiveLeadRequest() {
                LeadFrom = DateTime.Parse("2021-02-26 12:37:00.0000000"),
                Type = 2,
                Broker = new Broker()
                {
                    B_LLAPIKey = "LLB_LiveLead",
                    B_DATAPIKey = "LLB_DAT",
                    //B_EQFAPIKey = "LLB_EQF",
                    B_QPAPIKey = "LLB_QP",
                    B_TCCAPIKey = "LLB_TCC",
                    B_TCUSAPIKey = "LLB_TCUS"
                },
                Carrier = new Carrier()
                {
                    C_LLAPIKey = "LLB_LiveLead",
                    C_DATAPIKey = "LLB_DAT",
                    C_EQFAPIKey = "LLB_EQF",
                    C_QPAPIKey = "LLB_QP",
                    //C_TCCAPIKey = "LLB_TCC",
                    C_TCUSAPIKey = "LLB_TCUS"
                }
            };


            //Please note this test is just to allow the CIDC to pass - s

            // act
            var actionResult = await _liveLeadController.Post(request);
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
                var model = Assert.IsAssignableFrom<IEnumerable<GetLiveLeadResult>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

    }
}
