using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Queries;
using LoadLink.LoadMatching.Application.LoadPosting.Profiles;
using LoadLink.LoadMatching.Application.LoadPosting.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LoadPosting;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using LoadLink.LoadMatching.Api.Configuration;

namespace LoadLink.LoadMatching.Api.Test.LoadPosting
{
    public class LoadPostingControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ILoadPostingService _service;
        private readonly LoadPostingController _loadPostingController;
        private readonly IOptions<AppSettings> _settings;

        public LoadPostingControllerTest()
        {
            var userId = 34351;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            //AppSettings
            _settings = new DatabaseFixture().AppSettings();

            var mappingProfile = new LoadPostingProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappingProfile));
            var mapper = new Mapper(configuration);

            // integration            
            var repository = new LoadPostingRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LoadPostingService(repository, mapper);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);
           
            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _loadPostingController = new LoadPostingController(_service, _userHelper, _settings);
        }

        [Fact]
        public async Task LoadPostingController_Get_list()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";

            //act
            var actionResult = await _loadPostingController.GetList(apiKey);

            //assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLoadPostingQuery>>(viewResult.Value);

            Assert.NotNull(actionResult);

        }

        [Fact]
        public async Task LoadPostingController_Get()
        {

            //Arrange
            var apiKey = "LLC_EqPostingsView";
            var token = 4507700;

            // act
            var actionResult = await _loadPostingController.Get(token, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetLoadPostingQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(token, model.Token);
        }
        [Fact]
        public async Task LoadPostingController_Get_DAT()
        {

            //Arrange
            var apiKey = "LLC_EqPostingsView";
            var dat = true;

            // act
            var actionResult = await _loadPostingController.GetList(apiKey,dat);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetLoadPostingQuery>>(viewResult.Value);

            Assert.NotNull(model);
           
        }

        [Fact]
        public async Task LoadPostingController_Create_Sucess()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";
            var LoadPostingCommand = new CreateLoadPostingCommand
            {
                    DateAvail = DateTime.UtcNow,
                    SrceCity =  "London",
                    SrceSt =  "ON",
                    SrceRadius =  200,
                    DestCity =  "Chicago",
                    DestSt =  "IL",
                    DestRadius =  500,
                    VehicleSize =  "U",
                    VehicleType =  "FS",
                    Comment =  "ll test ",
                    PostMode =  "A",
                    ClientRefNum =  "Client Ref",
                    ProductName =  "WEBAPI",
                    PostingAttrib =  "",
                    NetworkId =  0,
                    GlobalExcluded =  true

            };

            // act
            var actionResult = await _loadPostingController.Post(LoadPostingCommand, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task LoadPostingController_Put_Status()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";
            var token = 29913736;
            var LoadPostingUpdateCommand = new UpdateLoadPostingCommand
            {
                PStatus = "A"
            };

            // act
            var actionResult = await _loadPostingController.Put(token, LoadPostingUpdateCommand, apiKey);

            // assert  
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task LoadPosting_Delete()
        {
            // arrange
            var tokenId = 29913901;
            var apiKey = "LLC_EqPostingsView";


            // act
            var actionResult = await _loadPostingController.Delete(tokenId, apiKey);

            // assert
            Assert.IsType<NoContentResult>(actionResult);
        }

    }
}
