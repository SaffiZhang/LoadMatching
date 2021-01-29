using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.VehicleAttribute.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAttributeController : ControllerBase
    {
        private readonly IUSMemberSearchService _vehicleAttributeService;

        public VehicleAttributeController(IUSMemberSearchService vehicleAttributeService)
        {
            _vehicleAttributeService = vehicleAttributeService;
        }

        [HttpGet("get-vehicle-Attribute-list")]
        public async Task<IActionResult> GetVehicleAttributeListAsync()
        {
            var result = await _vehicleAttributeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
