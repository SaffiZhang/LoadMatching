using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;
using System.Linq;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USCarrierSearchController : ControllerBase
    {
        private readonly IUSCarrierSearchService _USCarrierSearchService;
        private readonly IUserHelperService _userHelperService;

        public USCarrierSearchController(IUSCarrierSearchService USCarrierSearchService,
                                            IUserHelperService userHelperService)
        {
            _USCarrierSearchService = USCarrierSearchService;
            _userHelperService = userHelperService;
        }

        [HttpPost("{APIkey}/{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> GetUSCarrierSearchAsync([FromBody] GetUSCarrierSearchCommand searchRequest, 
                                                                 string APIkey, string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            if (searchRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check carrier search feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            USCarrierSearchSubscriptionsStatus subscriptions = new USCarrierSearchSubscriptionsStatus();
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            searchRequest.UserId = _userHelperService.GetUserId();
            
            var result = await _USCarrierSearchService.GetListAsync(searchRequest, subscriptions);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
