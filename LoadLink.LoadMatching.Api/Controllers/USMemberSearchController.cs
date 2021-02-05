﻿using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

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

        [HttpPost("us-member-search")]
        public async Task<IActionResult> GetUSMemberSearchAsync([FromBody] GetUSMemberSearchCommand searchRequest, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _USMemberSearchService.GetListAsync(searchRequest);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}