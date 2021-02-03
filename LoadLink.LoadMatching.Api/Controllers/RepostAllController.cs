using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.RepostAll.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepostAllController : ControllerBase
    {
        private readonly IRepostAllService _repostAllService;
        private readonly IUserHelperService _userHelperService;

        public RepostAllController(IRepostAllService repostAllService,
            IUserHelperService userHelperService)
        {
            _repostAllService = repostAllService;
            _userHelperService = userHelperService;
        }

        [HttpGet("repost-all")]
        public async Task<IActionResult> RepostAllAsync(string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var custCd = _userHelperService.GetCustCd();
            var userId = _userHelperService.GetUserId();

            var result = await _repostAllService.RepostAllAsync(custCd, userId);
            if (result == 0)
                return NoContent();

            return Ok(result);
        }
    }
}
