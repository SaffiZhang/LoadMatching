using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleSize.Profiles;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using LoadLink.LoadMatching.Persistence.Repositories.VehicleSize;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.VehicleSize
{
    public class VehicleSizeControllerTest
    {
        private readonly IVehicleSizeService _service;
        private readonly VehicleSizeController _vehicleSizeController;

        public VehicleSizeControllerTest()
        {
            var vehicleSizeProfile = new VehicleSizeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleSizeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new VehicleSizeRepository(new DatabaseFixture().ConnectionFactory);
            _service = new VehicleSizeService(repository, profile);

            // controller
            _vehicleSizeController = new VehicleSizeController(_service);
        }

        [Fact]
        public async Task GetVehicleSizeListAsync()
        {

            // act
            var actionResult = await _vehicleSizeController.GetVehicleSizeListAsync();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetVehicleSizeQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
