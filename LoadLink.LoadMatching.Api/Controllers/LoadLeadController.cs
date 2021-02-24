using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.LoadLead.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using LoadLink.LoadMatching.Api.Configuration;
using Microsoft.Extensions.Options;
using LoadLink.LoadMatching.Application.LoadLead.Models.Commands;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadLeadController : ControllerBase
    {
        private readonly ILoadLeadService _loadLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _settings;

        public LoadLeadController(ILoadLeadService LoadLeadService,
                                    IUserHelperService userHelperService,
                                    IOptions<AppSettings> settings)
        {
            _loadLeadService = LoadLeadService;
            _userHelperService = userHelperService;
            _settings = settings.Value;
        }

        [HttpGet("{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetListsAsync(string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var custCd = _userHelperService.GetCustCd();
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription 
            LoadLeadSubscriptionsStatus subscriptions = new LoadLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var result = await _loadLeadService.GetListAsync(custCd, mileageProvider, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{token}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetByPostingAsync(int token, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
            {
                return BadRequest("Invalid Load Token");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription 
            LoadLeadSubscriptionsStatus subscriptions = new LoadLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCd = _userHelperService.GetCustCd();

            var result = await _loadLeadService.GetByPostingAsync(custCd, token, mileageProvider, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{token}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetCombinedAsync(int token, string DATAPIkey, string QPAPIKey,
                                    string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
            {
                return BadRequest("Invalid Load Token");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription 
            LoadLeadSubscriptionsStatus subscriptions = new LoadLeadSubscriptionsStatus();
            subscriptions.HasDATSubscription = getUserApiKeys.Contains(DATAPIkey);
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;
            var leadsCap = _settings.AppSetting.LeadsCap;

            var custCd = _userHelperService.GetCustCd();

            var result = await _loadLeadService.GetByPosting_CombinedAsync(custCd, token, mileageProvider, leadsCap, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
