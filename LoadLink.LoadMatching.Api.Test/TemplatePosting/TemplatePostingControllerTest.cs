using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Queries;
using LoadLink.LoadMatching.Application.TemplatePosting.Profiles;
using LoadLink.LoadMatching.Application.TemplatePosting.Services;
using LoadLink.LoadMatching.Persistence.Repositories.TemplatePosting;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.TemplatePosting
{
    public class TemplatePostingControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly ITemplatePostingService _service;
        private readonly TemplatePostingController _templatePostingController;
        private readonly string apiKey = "LLB_LiveLead";

        public TemplatePostingControllerTest()
        {
            var userId = 34186;
            var custCd = "TCORELL";

            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var mappingProfile = new TemplatePostingProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappingProfile));
            var mapper = new Mapper(configuration);

            // integration            
            var repository = new TemplatePostingRepository(new DatabaseFixture().ConnectionFactory);
            _service = new TemplatePostingService(repository, mapper);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _templatePostingController = new TemplatePostingController(_service, _userHelper);
        }

        [Fact]
        public async Task Get_Template_Posting()
        {
            // arrange
            int templateId = 109999;

            //act
            var actionResult = await _templatePostingController.GetTemplatePostingAsync(templateId, apiKey);

            //assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetTemplatePostingQuery>(viewResult.Value);

            Assert.NotNull(model);
            Assert.Equal(templateId, model.TemplateID);
        }

        [Fact]
        public async Task Get_Template_Posting_List()
        {
            // act
            var actionResult = await _templatePostingController.GetTemplatePostingListAsync(apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetTemplatePostingQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Create_Template_Posting()
        {
            // arrange
            var templatePostingCommand = new TemplatePostingCommand
            {
                TemplateName = "TestMontTor",
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
            var actionResult = await _templatePostingController.CreateTemplatePostingAsync(templatePostingCommand, apiKey);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Update_Template_Posting()
        {
            // arrange
            var templatePostingCommand = new TemplatePostingCommand
            {
                TemplateID = 110008,
                TemplateName = "TestMontTor2",
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
            var actionResult = await _templatePostingController.UpdateTemplatePostingAsync(templatePostingCommand, apiKey);

            // assert  
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task Delete_Template_Posting()
        {
            // arrange
            short templateId = 1000;

            // act
            var actionResult = await _templatePostingController.DeleteTemplatePostingAsync(templateId, apiKey);

            // assert
            Assert.IsType<OkResult>(actionResult);
        }

    }
}
