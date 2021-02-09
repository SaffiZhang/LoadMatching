using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Application.PDRatio.Services;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDRatioController : ControllerBase
    {
        private readonly IPDRatioService _pdRatioService;
        private readonly IUserHelperService _userHelperService;

        public PDRatioController(IPDRatioService pdRatioService, IUserHelperService userHelperService)
        {
            _pdRatioService = pdRatioService;
            _userHelperService = userHelperService;
        }

        [HttpPost("{APIkey}")]
        public async Task<IActionResult> GetPDRatioAsync([FromBody] GetPDRatioCommand requestLane, string APIkey)
        {
            if (!(await _userHelperService.HasValidSubscription(APIkey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _pdRatioService.GetAsync(requestLane);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
