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
        private readonly IOptions<AppSettings> _appsettings;
        public EquipmentPostingControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

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
            _equipmentPostingController = new EquipmentPostingController(_service, _userHelper, _appsettings);
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
            var model = Assert.IsAssignableFrom<GetEquipmentPostingQuery>(viewResult.Value);

            Assert.NotNull(model);

        }

        [Fact]
        public async Task EquipmentPostingController_Get()
        {

            //Arrange
            var apiKey = "LLC_EqPostingsView";
            var token = 29913736;

            // act
            var actionResult = await _equipmentPostingController.Get(token, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetEquipmentPostingQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(token, model.Token);
        }

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
        public async Task Create_Equipment_Posting()
        {
            // arrange
            var equipmentPostingCommand = new CreateEquipmentPostingCommand
            {
                EquipmentName = "TestMontTor",
                UserId = 34351,
                PostType = "E",
                DateAvail = DateTime.Now,
                SrceID = 1003495,
                SrceCity = "Montreal",
                SrceSt = "QC",
                SrceRadius = 50,
                DestID = 1002770,
                DestCity = "Toronto",
                DestSt = "ON",
                DestRadius = 50,
                VehicleSize = "15",
                VehicleType = "1",
                Comment = "Montreal to Toronto",
                PostMode = "A",
                ClientRefNum = "Client Ref Number",
                PostingAttrib = "0",
                NetworkId = 0,
                Corridor = "C",
                CustCd = "TCORELL",
                CustomerTracking = false
            };

            // act
            var actionResult = await _equipmentPostingController.CreateEquipmentPostingAsync(equipmentPostingCommand, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Update_Equipment_Posting()
        {
            // arrange
            var equipmentPostingCommand = new EquipmentPostingCommand
            {
                EquipmentID = 110008,
                EquipmentName = "TestMontTor2",
                UserId = 34351,
                PostType = "E",
                DateAvail = DateTime.Now,
                SrceID = 1003495,
                SrceCity = "Montreal",
                SrceSt = "QC",
                SrceRadius = 50,
                DestID = 1002770,
                DestCity = "Toronto",
                DestSt = "ON",
                DestRadius = 50,
                VehicleSize = "15",
                VehicleType = "1",
                Comment = "Montreal to Toronto",
                PostMode = "A",
                ClientRefNum = "Client Ref Number",
                PostingAttrib = "0",
                NetworkId = 0,
                Corridor = "C",
                CustCd = "TCORELL",
                CustomerTracking = false
            };

            // act
            var actionResult = await _equipmentPostingController.UpdateEquipmentPostingAsync(equipmentPostingCommand, apiKey);

            // assert  
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task Delete_Equipment_Posting()
        {
            // arrange
            short equipmentId = 1000;

            // act
            var actionResult = await _equipmentPostingController.DeleteEquipmentPostingAsync(equipmentId, apiKey);

            // assert
            Assert.IsType<OkResult>(actionResult);
        }

    }
}
