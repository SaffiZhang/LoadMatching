using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.City.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService )
        {
            _cityService = cityService;
        }

        [HttpGet("{city}/{sortType}")]
        public async Task<IActionResult> GetAsync(string city, short sortType)
        {
            var result = await _cityService.GetListAsync(city, sortType);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
