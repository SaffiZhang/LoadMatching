using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models.Commands;
using LoadLink.LoadMatching.Api.Configuration;
using Microsoft.Extensions.Options;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatEquipmentLeadController : ControllerBase
    {
        private readonly IDatEquipmentLeadService _datEquipmentLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _settings;

        public DatEquipmentLeadController(IDatEquipmentLeadService datEquipmentLeadService, 
                                            IUserHelperService userHelperService,
                                            IOptions<AppSettings> settings)
        {
            _datEquipmentLeadService = datEquipmentLeadService;
            _userHelperService = userHelperService;
            _settings = settings.Value;
        }

        [HttpGet("{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetListAsync(string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(DATAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription 
            DatEquipmentSubscriptionsStatus subscriptions = new DatEquipmentSubscriptionsStatus();     
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCd = _userHelperService.GetCustCd();
            var leads = await _datEquipmentLeadService.GetListAsync(custCd, subscriptions, mileageProvider);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);
        }

        [HttpGet("{token}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetAsync(int token, string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {

            if (token <= 0)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(DATAPIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            // features subscription 
            DatEquipmentSubscriptionsStatus subscriptions = new DatEquipmentSubscriptionsStatus();      
            subscriptions.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey.ToUpper());
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            //AppSettings
            var mileageProvider = _settings.AppSetting.MileageProvider;

            var custCd = _userHelperService.GetCustCd();
            var leads = await _datEquipmentLeadService.GetAsyncByPosting(custCd, token, subscriptions, mileageProvider);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);

        }

    }
}
