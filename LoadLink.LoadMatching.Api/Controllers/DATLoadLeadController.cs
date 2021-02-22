using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATLoadLead.Services;
using LoadLink.LoadMatching.Persistence.Configuration;
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
            if (!getUserApiKeys.Contains(DATAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription statuses
            _datLoadLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datLoadLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datLoadLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datLoadLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCD = _userHelperService.GetCustCd();

            var result = await _datLoadLeadService.GetListAsync(custCD);

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
            if (!getUserApiKeys.Contains(DATAPIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            // features subscription statuses
            _datLoadLeadService.HasQPSubscription = getUserApiKeys.Contains(QPAPIKey);
            _datLoadLeadService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            _datLoadLeadService.HasTCCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            _datLoadLeadService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            var custCD = _userHelperService.GetCustCd();

            var result = await _datLoadLeadService.GetByPostingAsync(custCD, token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
