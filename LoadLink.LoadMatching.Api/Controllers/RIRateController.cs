using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Application.RIRate.Services;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RIRateController : ControllerBase
    {
        private readonly IRIRateService _riRateService;
        private readonly IUserHelperService _userHelperService;

        public RIRateController(IRIRateService riRateService, IUserHelperService userHelperService)
        {
            _riRateService = riRateService;
            _userHelperService = userHelperService;
        }

        [HttpPost("get-ri-rate")]
        public async Task<IActionResult> GetRIRateAsync([FromBody] GetRIRateCommand requestLane, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _riRateService.GetAsync(requestLane);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
