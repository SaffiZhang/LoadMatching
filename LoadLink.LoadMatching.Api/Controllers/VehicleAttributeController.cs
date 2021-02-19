using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleAttribute.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAttributeController : ControllerBase
    {
        private readonly IVehicleAttributeService _vehicleAttributeService;
        private readonly IUserHelperService _userHelperService;

        public VehicleAttributeController(IVehicleAttributeService vehicleAttributeService,
            IUserHelperService userHelperService)
        {
            _vehicleAttributeService = vehicleAttributeService;
            _userHelperService = userHelperService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _vehicleAttributeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
