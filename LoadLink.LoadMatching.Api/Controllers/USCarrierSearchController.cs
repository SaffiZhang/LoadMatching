using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USCarrierSearchController : ControllerBase
    {
        private readonly IUSCarrierSearchService _USCarrierSearchService;
        private readonly IUserHelperService _userHelperService;

        public USCarrierSearchController(
            IUSCarrierSearchService USCarrierSearchService,
            IUserHelperService userHelperService)
        {
            _USCarrierSearchService = USCarrierSearchService;
            _userHelperService = userHelperService;
        }

        [HttpPost("us-carrier-search")]
        public async Task<IActionResult> GetUSCarrierSearchAsync([FromBody] GetUSCarrierSearchCommand searchRequest)
        {
            if (searchRequest == null)
                return BadRequest();

            searchRequest.UserId = _userHelperService.GetUserId();
            
            var result = await _USCarrierSearchService.GetListAsync(searchRequest);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
