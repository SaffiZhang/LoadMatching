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
        private readonly AppSettings _appSettings;

        public LiveLeadController(ILiveLeadService liveLeadService, 
                                    IUserHelperService userHelperService, 
                                    IOptions<AppSettings> appSettings)
        {
            _liveLeadService = liveLeadService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] GetLiveLeadRequest LLRequest)
        {
            if (LLRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check subscription - load leads (Type 0)
            if (LLRequest.Type == 0 && !getUserApiKeys.Contains(LLRequest.Broker.B_LLAPIKey))
                return Ok(ResponseCode.NotSubscribe);
            // check subscription - equipment leads (Type 1)
            else if (LLRequest.Type == 1 && !getUserApiKeys.Contains(LLRequest.Carrier.C_LLAPIKey))
                return Ok(ResponseCode.NotSubscribe);
            // check subscription - Both Load & Equipment leads (Type 2)
            else if (LLRequest.Type == 2 && (!getUserApiKeys.Contains(LLRequest.Broker.B_LLAPIKey) || !getUserApiKeys.Contains(LLRequest.Carrier.C_LLAPIKey)))
                return Ok(ResponseCode.NotSubscribe);

            //features subscription statuses
            LiveLeadSubscriptionsStatus subscriptions = new LiveLeadSubscriptionsStatus();
            subscriptions.B_DATAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_DATAPIKey);
            subscriptions.B_EQFAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_EQFAPIKey);
            subscriptions.B_QPAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_QPAPIKey);
            subscriptions.B_TCCAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_TCCAPIKey);
            subscriptions.B_TCUSAPIKey_Status = getUserApiKeys.Contains(LLRequest.Broker.B_TCUSAPIKey);
            subscriptions.C_DATAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_DATAPIKey);
            subscriptions.C_EQFAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_EQFAPIKey);
            subscriptions.C_QPAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_QPAPIKey);
            subscriptions.C_TCCAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_TCCAPIKey);
            subscriptions.C_TCUSAPIKey_Status = getUserApiKeys.Contains(LLRequest.Carrier.C_TCUSAPIKey);

            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var LiveLeads = await _liveLeadService.GetLiveLeads(LLRequest, mileageProvider, custCd, subscriptions);

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
