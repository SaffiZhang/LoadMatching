using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentPosting.Profiles;
using LoadLink.LoadMatching.Application.EquipmentPosting.Services;
using LoadLink.LoadMatching.Persistence.Repositories.EquipmentPosting;
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

namespace LoadLink.LoadMatching.Api.Test.EquipmentPosting
{
    public class EquipmentPostingControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IEquipmentPostingService _service;
        private readonly EquipmentPostingController _equipmentPostingController;
 
        public EquipmentPostingControllerTest()
        {
            var userId = 34351;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);
            AppSettings appSettings = new AppSettings() { MileageProvider = "P" };
            IOptions<AppSettings> options = Options.Create(appSettings);


            var mappingProfile = new EquipmentPostingProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappingProfile));
            var mapper = new Mapper(configuration);

            // integration            
            var repository = new EquipmentPostingRepository(new DatabaseFixture().ConnectionFactory);
            _service = new EquipmentPostingService(repository, mapper);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);
           
            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _equipmentPostingController = new EquipmentPostingController(_service, _userHelper, options);
        }

        [Fact]
        public async Task EquipmentPostingController_Get_list()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";

            //act
            var actionResult = await _equipmentPostingController.GetList(apiKey);

            //assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentPostingQuery>>(viewResult.Value);

            Assert.NotNull(actionResult);

        }

        [Fact]
        public async Task EquipmentPostingController_Get()
        {

            //Arrange
            var apiKey = "LLC_EqPostingsView";
            var token = 29913902;

            // act
            var actionResult = await _equipmentPostingController.Get(token, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetEquipmentPostingQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(token, model.Token);
        }

        [Fact]
        public async Task EquipmentPostingController_Get_DAT()
        {

            //Arrange
            var apiKey = "LLC_EqPostingsView";
            var dat = true;

            // act
            var actionResult = await _equipmentPostingController.GetList(apiKey,dat);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetEquipmentPostingQuery>>(viewResult.Value);

            Assert.NotNull(model);
           
        }

        [Fact]
        public async Task EquipmentPostingController_Create_Sucess()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";
            var equipmentPostingCommand = new CreateEquipmentPostingCommand
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
                    Corridor =  "C",
                    GlobalExcluded =  true,
                    CustomerTracking =  false

            };

            // act
            var actionResult = await _equipmentPostingController.Post(equipmentPostingCommand, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task EquipmentPostingController_Put_Status()
        {
            // arrange
            var apiKey = "LLC_EqPostingsView";
            var token = 29913736;
            var equipmentPostingUpdateCommand = new UpdateEquipmentPostingCommand
            {
                PStatus = "A"
            };

            // act
            var actionResult = await _equipmentPostingController.Put(token, equipmentPostingUpdateCommand, apiKey);

            // assert  
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task EquipmentPosting_Delete()
        {
            // arrange
            var tokenId = 29913901;
            var apiKey = "LLC_EqPostingsView";


            // act
            var actionResult = await _equipmentPostingController.Delete(tokenId, apiKey);

            // assert
            Assert.IsType<NoContentResult>(actionResult);
        }

    }
}
