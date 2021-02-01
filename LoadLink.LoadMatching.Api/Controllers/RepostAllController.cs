using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.RepostAll.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RepostAllAsync()
        {
            var custCd = ""; //_userHelperService.GetCustCd();
            var userId = _userHelperService.GetUserId();

            var result = await _repostAllService.RepostAllAsync(custCd, userId);
            if (result == 0)
                return NoContent();

            return Ok(result);
        }
    }
}
