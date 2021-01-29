using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USMemberSearchController : ControllerBase
    {
        private readonly IUSMemberSearchService _USMemberSearchService;

        public USMemberSearchController(IUSMemberSearchService USMemberSearchService)
        {
            _USMemberSearchService = USMemberSearchService;
        }

        [HttpPost("us-member-search")]
        public async Task<IActionResult> GetUSMemberSearchAsync([FromBody] GetUSMemberSearchCommand searchRequest)
        {
            var result = await _USMemberSearchService.GetListAsync(searchRequest);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
