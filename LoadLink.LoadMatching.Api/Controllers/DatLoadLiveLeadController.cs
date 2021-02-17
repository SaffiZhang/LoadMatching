using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
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
        private readonly IOptions<AppSettings> _appSettings;

        public DatLoadLiveLeadController(IDatLoadLiveLeadService datLoadLiveLeadService, IUserHelperService userHelperService, IOptions<AppSettings> appSettings)
        {
            _datLoadLiveLeadService = datLoadLiveLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings;
        }

        [HttpGet("leadfrom/{leadfrom}/{LLAPIkey}/{DATAPIkey}/{QPAPIKey}/{EQFAPIKey}/{TCUSAPIKey}/{TCCAPIKey}")]
        public async Task<IActionResult> GetList(DateTime leadfrom, string LLAPIkey, string DATAPIkey, string QPAPIKey, string EQFAPIKey, string TCUSAPIKey, string TCCAPIKey)
        {
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(LLAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription 
            _datLoadLiveLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datLoadLiveLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datLoadLiveLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datLoadLiveLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.Value.MileageProvider;
            var leads = await _datLoadLiveLeadService.GetLeadsAsync(custCd, mileageProvider,leadfrom,null);

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
                return BadRequest();
            if (leadfrom <= DateTime.MinValue)
                return BadRequest("leadfrom must be valid date format.");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription
            if (!getUserApiKeys.Contains(LLAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription 
            _datLoadLiveLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datLoadLiveLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datLoadLiveLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datLoadLiveLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.Value.MileageProvider;
            var leads = await _datLoadLiveLeadService.GetLeadsAsync(custCd, mileageProvider, leadfrom, token);

            if (leads == null)
            {
                return NoContent();
            }

            return Ok(leads);

        }

    }
}
