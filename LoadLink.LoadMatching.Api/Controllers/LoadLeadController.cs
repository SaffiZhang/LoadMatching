using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.LoadLead.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;
using System.Linq;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadLeadController : ControllerBase
    {
        private readonly ILoadLeadService _loadLeadService;
        private readonly IUserHelperService _userHelperService;

        public LoadLeadController(
            ILoadLeadService LoadLeadService,
            IUserHelperService userHelperService)
        {
            _loadLeadService = LoadLeadService;
            _userHelperService = userHelperService;
        }

        [HttpGet("get-list-async")]
        public async Task<IActionResult> GetListsAsync(string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = "P";
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            _loadLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _loadLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _loadLeadService.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _loadLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var result = await _loadLeadService.GetListAsync(custCd, mileageProvider);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("get-by-posting-async")]
        public async Task<IActionResult> GetByPostingAsync(int token, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
            {
                return BadRequest("Invalid Load Token");
            }

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = "P";
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            _loadLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _loadLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _loadLeadService.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _loadLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var result = await _loadLeadService.GetByPostingAsync(token, custCd, mileageProvider);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("get-combined-async")]
        public async Task<IActionResult> GetCombinedAsync(int token, string DATAPIkey, string QPAPIKey,
                                    string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
            {
                return BadRequest("Invalid Load Token");
            }

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = "P";
            var leadsCap = 500;
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            _loadLeadService.HasDATStatusEnabled = getUserApiKeys.Contains(DATAPIkey);
            _loadLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _loadLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _loadLeadService.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _loadLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var result = await _loadLeadService.GetByPosting_CombinedAsync(token, custCd, mileageProvider, leadsCap);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
