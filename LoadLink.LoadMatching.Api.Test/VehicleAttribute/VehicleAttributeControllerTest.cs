using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleAttribute.Profiles;
using LoadLink.LoadMatching.Application.VehicleAttribute.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleAttribute;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleAttribute
{
    public class USMemberSearchControllerTest
    {
        private readonly IUSMemberSearchService _service;
        private readonly VehicleAttributeController _vehicleAttributeController;

        public USMemberSearchControllerTest()
        {
            var vehicleAttributeProfile = new USMemberSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleAttributeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USMemberSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USMemberSearchService(repository, profile);

            // controller
            _vehicleAttributeController = new VehicleAttributeController(_service);
        }

        [Fact]
        public async Task GetVehicleAttributeListAsync()
        {

            // act
            var actionResult = await _vehicleAttributeController.GetVehicleAttributeListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleAttributeQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
