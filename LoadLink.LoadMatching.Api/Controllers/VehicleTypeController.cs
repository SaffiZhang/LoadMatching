using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpGet("get-vehicle-type-list")]
        public async Task<IActionResult> GetVehicleTypeListAsync()
        {
            var result = await _vehicleTypeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
