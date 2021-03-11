using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.CarrierSearch.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierSearchController : ControllerBase
    {
        private readonly ICarrierSearchService _carrierSerarchService;
        private readonly IUserHelperService _userHelperService;

        public CarrierSearchController(ICarrierSearchService carrierSearchService, 
                                        IUserHelperService userHelperService )
        {
            _userHelperService = userHelperService;
            _carrierSerarchService = carrierSearchService;
        }
                            
        [HttpPost("{APIkey}/{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> SearchAsync([FromBody] GetCarrierSearchRequest searchrequest, string APIkey, string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            if (searchrequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();
                
            // check carrier search feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            CarrierSearchSubscriptionsStatus subscriptions = new CarrierSearchSubscriptionsStatus();
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            searchrequest.UserID = _userHelperService.GetUserId();

            var result = await _carrierSerarchService.GetCarrierSearchAsync(searchrequest, subscriptions);

            // not found
            if (result == null)
                return NoContent();

            return Ok(result);
        }    
    }
}
