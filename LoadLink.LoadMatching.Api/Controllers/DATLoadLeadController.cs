using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATLoadLead.Models.Commands;
using LoadLink.LoadMatching.Application.DATLoadLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatLoadLeadController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IDatLoadLeadService _datLoadLeadService;
        private readonly IUserHelperService _userHelperService;

        public DatLoadLeadController(IDatLoadLeadService datLoadLeadService,
                                        IUserHelperService userHelperService,
                                        IOptions<AppSettings> settings)
        {
            _datLoadLeadService = datLoadLeadService;
            _userHelperService = userHelperService;
            _settings = settings.Value;
        }

        [HttpGet("{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetListAsync(string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check DAT feature access
            if (!getUserApiKeys.Contains(DATAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription statuses
            DatLoadLeadSubscriptionsStatus subscriptions = new DatLoadLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCD = _userHelperService.GetCustCd();

            var result = await _datLoadLeadService.GetListAsync(custCD, mileageProvider, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{token}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetByPostingAsync(int token, string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
                return BadRequest("Invalid Load Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check DAT feature access
            if (!getUserApiKeys.Contains(DATAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription statuses
            DatLoadLeadSubscriptionsStatus subscriptions = new DatLoadLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCD = _userHelperService.GetCustCd();

            var result = await _datLoadLeadService.GetByPostingAsync(custCD, token, mileageProvider, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
