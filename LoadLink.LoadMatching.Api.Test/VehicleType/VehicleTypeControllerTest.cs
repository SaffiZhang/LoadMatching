using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Profiles;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleType;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleType
{
    public class VehicleSizeControllerTest
    {
        private readonly IVehicleTypeService _service;
        private readonly VehicleTypeController _vehicleTypeController;

        public VehicleSizeControllerTest()
        {
            var vehicleTypeProfile = new VehicleTypeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleTypeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new VehicleTypeRepository(new DatabaseFixture().ConnectionFactory);
            _service = new VehicleTypeService(repository, profile);

            // controller
            _vehicleTypeController = new VehicleTypeController(_service);
        }

        [Fact]
        public async Task GetVehicleTypeListAsync()
        {

            // act
            var actionResult = await _vehicleTypeController.GetVehicleTypeListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleTypesQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
