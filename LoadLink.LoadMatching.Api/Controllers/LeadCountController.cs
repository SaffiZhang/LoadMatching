using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LeadCount.Services;

using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadCountController : ControllerBase
    {
        private readonly ILeadsCountService _leadsCountService;
        private readonly IUserHelperService _userHelperService;

        public LeadCountController(ILeadsCountService leadsCountService,
                                    IUserHelperService userHelperService)
        {
            _leadsCountService = leadsCountService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{token}/{type}/{DATAPIKey}")]
        public async Task<IActionResult> GetLeadsCountAsync(int token, string type, string DATAPIKey)
        {
            bool getDAT = false;

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // DAT features subscription status
            getDAT = getUserApiKeys.Contains(DATAPIKey.ToUpper());

            var userId = _userHelperService.GetUserId();

            var result = await _leadsCountService.GetLeadsCountAsync(userId, token, getDAT, type);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
