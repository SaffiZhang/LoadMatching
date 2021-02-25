using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.EquipmentLead.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentLeadController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IEquipmentLeadService _equipmentLeadService;
        private readonly IUserHelperService _userHelperService;

        public EquipmentLeadController(IEquipmentLeadService equipmentLeadService,
                                        IUserHelperService userHelperService,
                                        IOptions<AppSettings> settings)
        {
            _equipmentLeadService = equipmentLeadService;
            _userHelperService = userHelperService;
            _settings = settings.Value;
        }

        [HttpGet("{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetListAsync(string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription statuses
            EquipmentLeadSubscriptionsStatus subscriptions = new EquipmentLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCD = _userHelperService.GetCustCd();

            var result = await _equipmentLeadService.GetListAsync(custCD, mileageProvider, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{token}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetByPostingAsync(int token, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (token <= 0)
                return BadRequest("Invalid Equipment Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription statuses
            EquipmentLeadSubscriptionsStatus subscriptions = new EquipmentLeadSubscriptionsStatus();
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCD = _userHelperService.GetCustCd();

            var result = await _equipmentLeadService.GetByPostingAsync(custCD, token, mileageProvider, subscriptions);

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
                return BadRequest("Invalid Equipment Token");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // features subscription statuses
            EquipmentLeadSubscriptionsStatus subscriptions = new EquipmentLeadSubscriptionsStatus();
            subscriptions.HasDATSubscription = getUserApiKeys.Contains(DATAPIkey);
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;
            var leadsCap = _settings.AppSetting.LeadsCap;

            var custCD = _userHelperService.GetCustCd();

            var result = await _equipmentLeadService.GetCombinedAsync(custCD, token, mileageProvider, leadsCap, subscriptions);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
