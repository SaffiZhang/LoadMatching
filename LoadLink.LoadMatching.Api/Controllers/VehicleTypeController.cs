using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleType.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

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

        [HttpGet("get-vehicle-type-list")]
        public async Task<IActionResult> GetVehicleTypeListAsync(string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _vehicleTypeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
