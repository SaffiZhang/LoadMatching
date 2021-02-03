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
        private readonly IUSMemberSearchService _vehicleAttributeService;
        private readonly IUserHelperService _userHelperService;

        public VehicleAttributeController(IUSMemberSearchService vehicleAttributeService,
            IUserHelperService userHelperService)
        {
            _vehicleAttributeService = vehicleAttributeService;
            _userHelperService = userHelperService;
        }

        [HttpGet("get-vehicle-Attribute-list")]
        public async Task<IActionResult> GetVehicleAttributeListAsync(string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _vehicleAttributeService.GetListAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
