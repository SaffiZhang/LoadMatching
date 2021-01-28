using System.Threading.Tasks;
using AutoMapper;
using LoadLink.LoadMatching.Application.VehicleSize.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleSizeController : ControllerBase
    {
        private readonly IVehicleSizeService _vehicleSizeService;
        private readonly IMapper _mapper;

        public VehicleSizeController(IVehicleSizeService vehicleSizeService)
        {
            _vehicleSizeService = vehicleSizeService;
        }

        [HttpGet("get-vehicle-size-list")]
        public async Task<IActionResult> GetVehicleSizeListAsync()
        {
            var result = await _vehicleSizeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
