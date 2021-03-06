
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.MemberSearch.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberSearchController : ControllerBase
    {
        private readonly IMemberSearchService _memberSearchService;
        private readonly IUserHelperService _userHelperService;

        public MemberSearchController(IMemberSearchService memberSearchService,
                                        IUserHelperService userHelperService)
        {
            _memberSearchService = memberSearchService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> Search([FromQuery] GetMemberSearchRequest searchRequest,  
                                                string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            if (searchRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            MemberSearchSubscriptionsStatus subscriptions = new MemberSearchSubscriptionsStatus();
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            if (string.IsNullOrEmpty(searchRequest.CustCd))
                searchRequest.CustCd = _userHelperService.GetCustCd();

            var result = await _memberSearchService.GetMemberSearch(searchRequest, subscriptions);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
        
        [HttpPost("{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> MemberSearchRequest([FromBody] GetMemberSearchRequest searchRequest,  
                                                                string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            if (searchRequest == null)
                return BadRequest();

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            MemberSearchSubscriptionsStatus subscriptions = new MemberSearchSubscriptionsStatus();
            subscriptions.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
            subscriptions.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
            subscriptions.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

            if (string.IsNullOrEmpty(searchRequest.CustCd))
                searchRequest.CustCd = _userHelperService.GetCustCd();

            var result = await _memberSearchService.GetMemberSearch(searchRequest, subscriptions);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
