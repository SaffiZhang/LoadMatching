using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleSizeController : ControllerBase
    {
        private readonly IVehicleSizeService _vehicleSizeService;
        private readonly IUserHelperService _userHelperService;

        public VehicleSizeController(IVehicleSizeService vehicleSizeService,
                                        IUserHelperService userHelperService)
        {
            _vehicleSizeService = vehicleSizeService;
            _userHelperService = userHelperService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _vehicleSizeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("PostType/{postType}")]
        public async Task<IActionResult> GetListByPostTypeAsync(string postType)
        {
            if (String.IsNullOrEmpty(postType))
                return BadRequest("Invalid PostType");

            var result = await _vehicleSizeService.GetListByPostTypeAsync(postType);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
