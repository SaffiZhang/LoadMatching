using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Persistence.Configuration;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatEquipmentLeadController : ControllerBase
    {
        private readonly IDatEquipmentLeadService _datEquipmentLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _appSettings;

        public DatEquipmentLeadController(IDatEquipmentLeadService datEquipmentLeadService, 
                                            IUserHelperService userHelperService, 
                                            IOptions<AppSettings> appSettings)
        {
            _datEquipmentLeadService = datEquipmentLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetListAsync(string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(DATAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription 
            _datEquipmentLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datEquipmentLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datEquipmentLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datEquipmentLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var leads = await _datEquipmentLeadService.GetListAsync(custCd);

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
            if (!getUserApiKeys.Contains(DATAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription 
            _datEquipmentLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datEquipmentLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datEquipmentLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datEquipmentLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var leads = await _datEquipmentLeadService.GetAsyncByPosting(custCd, token);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);

        }

    }
}
