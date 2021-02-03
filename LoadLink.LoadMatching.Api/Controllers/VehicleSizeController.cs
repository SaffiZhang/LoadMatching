using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleSizeController : ControllerBase
    {
        private readonly IVehicleAttributeService _vehicleSizeService;
        private readonly IUserHelperService _userHelperService;

        public VehicleSizeController(IVehicleAttributeService vehicleSizeService,
            IUserHelperService userHelperService)
        {
            _vehicleSizeService = vehicleSizeService;
            _userHelperService = userHelperService;
        }

        [HttpGet("get-vehicle-size-list")]
        public async Task<IActionResult> GetVehicleSizeListAsync(string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _vehicleSizeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
