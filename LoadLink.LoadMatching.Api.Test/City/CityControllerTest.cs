using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.City.Models.Commands;
using LoadLink.LoadMatching.Application.City.Profiles;
using LoadLink.LoadMatching.Application.City.Services;
using LoadLink.LoadMatching.Persistence.Repositories.City;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.City
{
    public class CityControllerTest
    {
        private readonly ICityService _service;
        private readonly CityController _cityController;

        public CityControllerTest()
        {
        
            var vehicleSizeProfile = new CityProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(vehicleSizeProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new CityRepository(new DatabaseFixture().ConnectionFactory);
            _service = new CityService(repository, profile);

            // controller
            _cityController = new CityController(_service);
        }

        [Fact]
        public async Task GetCityListAsync()
        {
            //arrange
            var city = "toro";
            short sortType = 1;

            // act
            var actionResult = await _cityController.GetAsync(city, sortType);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetCityCommand>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
