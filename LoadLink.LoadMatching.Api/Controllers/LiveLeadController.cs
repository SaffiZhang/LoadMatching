using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LiveLead.Models.Commands;
using LoadLink.LoadMatching.Application.LiveLead.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveLeadController : ControllerBase
    {
        private readonly ILiveLeadService _liveLeadService;
        private readonly IUserHelperService _userHelperService;
        private readonly IOptions<AppSettings> _appSettings;

        public LiveLeadController(ILiveLeadService liveLeadService, IUserHelperService userHelperService, IOptions<AppSettings> appSettings)
        {
            _liveLeadService = liveLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] GetLiveLeadRequest LLRequest)
        {
            if (LLRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription - load leads
            if (LLRequest.Type == 0 && !getUserApiKeys.Contains(LLRequest.Broker.B_LLAPIKey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);
            // check subscription - equipment leads
            else if (LLRequest.Type == 1 && !getUserApiKeys.Contains(LLRequest.Carrier.C_LLAPIKey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);


            _liveLeadService.B_DATAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_DATAPIKey);
            _liveLeadService.B_EQFAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_EQFAPIKey);
            _liveLeadService.B_QPAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_QPAPIKey);
            _liveLeadService.B_TCCAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_TCCAPIKey);
            _liveLeadService.B_TCUSAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_TCUSAPIKey);
            _liveLeadService.C_DATAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_DATAPIKey);
            _liveLeadService.C_EQFAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_EQFAPIKey);
            _liveLeadService.C_QPAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_QPAPIKey);
            _liveLeadService.C_TCCAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_TCCAPIKey);
            _liveLeadService.C_TCUSAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.Value.MileageProvider;
            var LiveLeads = await _liveLeadService.GetLiveLeads(LLRequest,mileageProvider,custCd);

            if (LiveLeads == null)
                return NoContent();

            return Ok(LiveLeads);

        }

        [HttpGet("ServerTime")]
        public async Task<IActionResult> GetServerTime()
        {

            DateTime ServTime;
            ServTime = await _liveLeadService.GetServerTime();

            return Ok(ServTime);

        }
    }
}
