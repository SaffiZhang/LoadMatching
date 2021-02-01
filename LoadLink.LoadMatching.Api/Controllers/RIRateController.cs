using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.RIRate.Services;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RIRateController : ControllerBase
    {
        private readonly IRIRateService _riRateService;

        public RIRateController(IRIRateService riRateService)
        {
            _riRateService = riRateService;
        }

        [HttpPost("get-ri-rate")]
        public async Task<IActionResult> GetRIRateAsync([FromBody] GetRIRateCommand requestLane)
        {
            var result = await _riRateService.GetAsync(requestLane);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
