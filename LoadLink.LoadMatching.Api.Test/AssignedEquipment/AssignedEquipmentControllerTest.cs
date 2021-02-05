using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Queries;
using LoadLink.LoadMatching.Application.AssignedEquipment.Profiles;
using LoadLink.LoadMatching.Application.AssignedEquipment.Services;
using LoadLink.LoadMatching.Persistence.Repositories.AssignedEquipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.AssignedEquipment
{
    public class AssignedEquipmentControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IAssignedEquipmentService _service;
        private readonly AssignedEquipmentController _assignedEquipmentController;

        public AssignedEquipmentControllerTest()
        {
            var userId = 41590;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            var assignedEquipmentProfile = new AssignedEquipmentProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(assignedEquipmentProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new AssignedEquipmentRepository(new DatabaseFixture().ConnectionFactory);
            _service = new AssignedEquipmentService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, null);
            _assignedEquipmentController = new AssignedEquipmentController(_service, _userHelper);
        }

        [Fact]
        public async Task Get_AssignedEquipment()
        {
            //arrange
            int token = 29913706;

            // act
            var actionResult = await _assignedEquipmentController.GetAsync(token);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetAssignedLoadQuery>(viewResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Update_AssignedEquipment()
        {
            // arrange
            var updateCmd = new UpdateAssignedEquipmentCommand
            {
                PIN = "693349842",
                EToken = 29913706,
                DriverID = 41590
            };

            // act
            var actionResult = await _assignedEquipmentController.UpdateAsync(updateCmd);

            // assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
