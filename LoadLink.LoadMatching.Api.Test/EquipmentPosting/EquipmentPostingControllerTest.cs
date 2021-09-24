
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;

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
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using MediatR;


namespace LoadLink.LoadMatching.Api.Test.EquipmentPosting
{
    public class EquipmentPostingControllerTest
    {
       
        private Mock<IUserHelperService> _mockUserHelper;
        private Mock<IMediator> _mockMediator;
        private List<string> _apiKeys = new List<string>() { "LLC_EqPostingsView" };


        [Fact]
        public async Task EquipmentPostingController_Create_Sucess()
        {
            // arrange
            SetupController();
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

            var controller = new EquipmentPostingController(_mockUserHelper.Object, _mockMediator.Object);
            var actionResult = await controller.Post(equipmentPostingCommand, "");

            // assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(actionResult);
          
           
        }
        private void SetupController()
        {
            _mockUserHelper = new Mock<IUserHelperService>();
            _mockMediator = new Mock<IMediator>();
            _mockUserHelper.Setup(m => m.GetUserApiKeys()).ReturnsAsync(_apiKeys);
            _mockUserHelper.Setup(m => m.GetCustCd()).Returns("a");
            _mockUserHelper.Setup(m => m.GetUserId()).Returns(1);
            _mockMediator.Setup(m => m.Send(It.IsAny<IRequest<IEnumerable<LeadBase>>>(), new System.Threading.CancellationToken()))
                .ReturnsAsync(new List<LeadBase>());

        }
      

    }
}
