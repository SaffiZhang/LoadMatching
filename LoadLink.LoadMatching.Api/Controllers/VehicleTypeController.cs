using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IUserHelperService _userHelperService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService,
            IUserHelperService userHelperService)
        {
            _vehicleTypeService = vehicleTypeService;
            _userHelperService = userHelperService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _vehicleTypeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
