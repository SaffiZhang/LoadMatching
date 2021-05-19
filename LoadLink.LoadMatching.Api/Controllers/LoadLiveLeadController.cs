using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LoadLiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LoadLiveLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadLiveLeadController : ControllerBase
    {
        private readonly ILoadLiveLeadService _loadLiveLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _appSettings;

        public LoadLiveLeadController(ILoadLiveLeadService loadLiveLeadService, 
                                        IUserHelperService userHelperService, 
                                        IOptions<AppSettings> appSettings)
        {
            _loadLiveLeadService = loadLiveLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("leadfrom/{leadfrom}/{LLAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetList(DateTime leadfrom, string LLAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(LLAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription
            LoadLiveLeadSubscriptionsStatus subscriptions = new LoadLiveLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var leads = await _loadLiveLeadService.GetLeadsAsync(custCd, mileageProvider, leadfrom, null, subscriptions);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);
        }

        [HttpGet("{token}/leadfrom/{leadfrom}/{LLAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetByToken(DateTime leadfrom, int? token, string LLAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token < 0)
                return BadRequest();
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(LLAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription 
            LoadLiveLeadSubscriptionsStatus subscriptions = new LoadLiveLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var leads = await _loadLiveLeadService.GetLeadsAsync(custCd, mileageProvider, leadfrom, token, subscriptions);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);
        }
    }
}
