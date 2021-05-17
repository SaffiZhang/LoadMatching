using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;
using System.Linq;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USMemberSearchController : ControllerBase
    {
        private readonly IUSMemberSearchService _USMemberSearchService;
        private readonly IUserHelperService _userHelperService;
        public USMemberSearchController(IUSMemberSearchService USMemberSearchService,
                                        IUserHelperService userHelperService)
        {
            _USMemberSearchService = USMemberSearchService;
            _userHelperService = userHelperService;
        }

        [HttpPost("{APIkey}/{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> GetUSMemberSearchAsync([FromBody] GetUSMemberSearchCommand searchRequest, 
                                                                string APIkey, string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            if (searchRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check carrier search feature access
            if (!getUserApiKeys.Contains(APIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            USMemberSearchSubscriptionsStatus subscriptions = new USMemberSearchSubscriptionsStatus();
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey.ToUpper());
            subscriptions.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey.ToUpper());
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey.ToUpper());

            if (string.IsNullOrEmpty(searchRequest.CustCd))
                searchRequest.CustCd = _userHelperService.GetCustCd();

            var result = await _USMemberSearchService.GetListAsync(searchRequest, subscriptions);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
