﻿using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
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

        public CarrierSearchController(ICarrierSearchService carrierSearchService, IUserHelperService userHelperService )
        {
            _userHelperService = userHelperService;
            _carrierSerarchService = carrierSearchService;
        }

        [HttpPost("{APIkey}/{EQFAPIKey}/{TCCAPIKey}/{TCUSAPIKey}")]
        public async Task<IActionResult> Search([FromBody] GetCarrierSearchRequest searchrequest, string APIkey, string EQFAPIKey, string TCCAPIKey, string TCUSAPIKey)
        {
            try
            {
                var getUserApiKeys = await _userHelperService.GetUserApiKeys();
                
                // check carrier search feature access
                if (!getUserApiKeys.Contains(APIkey))
                    throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

                _carrierSerarchService.HasEQSubscription = getUserApiKeys.Contains(EQFAPIKey);
                _carrierSerarchService.HasTCSubscription = getUserApiKeys.Contains(TCCAPIKey);
                _carrierSerarchService.HasTCUSSubscription = getUserApiKeys.Contains(TCUSAPIKey);

                var result = await _carrierSerarchService.GetCarrierSearch(searchrequest, _userHelperService.GetUserId());

                // not found
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return BadRequest($"An exception has occurred.  Please contact support. {ex}");
            }
        }

    
    }
}