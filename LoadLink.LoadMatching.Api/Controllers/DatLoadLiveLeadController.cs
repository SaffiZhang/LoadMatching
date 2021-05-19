using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatLoadLiveLeadController : ControllerBase
    {
        private readonly IDatLoadLiveLeadService _datLoadLiveLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _appSettings;

        public DatLoadLiveLeadController(IDatLoadLiveLeadService datLoadLiveLeadService, 
                                            IUserHelperService userHelperService, 
                                            IOptions<AppSettings> appSettings)
        {
            _datLoadLiveLeadService = datLoadLiveLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("leadfrom/{leadfrom}/{LLAPIkey}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetList(DateTime leadfrom, string LLAPIkey, string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // CHECK IF USER IS SUBSCRIBED FOR LiveLead FEATURE & DAT FEATURE
            if (!(getUserApiKeys.Contains(LLAPIkey) && getUserApiKeys.Contains(DATAPIkey)))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription 
            DatLoadLiveLeadSubscriptionsStatus subscriptions = new DatLoadLiveLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var custCd = _userHelperService.GetCustCd();

            var leads = await _datLoadLiveLeadService.GetLeadsAsync(custCd, mileageProvider, leadfrom, null, subscriptions);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);
        }

        [HttpGet("{token}/leadfrom/{leadfrom}/{LLAPIkey}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetByToken(DateTime leadfrom, int? token, string LLAPIkey, string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token < 0)
                return BadRequest("Invalid Load Token.");
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // CHECK IF USER IS SUBSCRIBED FOR LiveLead FEATURE & DAT FEATURE
            if (!(getUserApiKeys.Contains(LLAPIkey) && getUserApiKeys.Contains(DATAPIkey)))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription 
            DatLoadLiveLeadSubscriptionsStatus subscriptions = new DatLoadLiveLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var custCd = _userHelperService.GetCustCd();

            var leads = await _datLoadLiveLeadService.GetLeadsAsync(custCd, mileageProvider, leadfrom, token, subscriptions);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);
        }
    }
}
