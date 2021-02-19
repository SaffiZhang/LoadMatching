using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LegacyDeleted.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegacyDeletedController : ControllerBase
    {
        private readonly ILegacyDeletedService _legacyDeletedService;
        private readonly IUserHelperService _userHelperService;

        public LegacyDeletedController(ILegacyDeletedService legacyDeletedService,
                                        IUserHelperService userHelperService)
        {
            _legacyDeletedService = legacyDeletedService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{leadType}")]
        public async Task<IActionResult> GetListAsync(char leadType)
        {
            if (string.IsNullOrEmpty(leadType.ToString()))
                return BadRequest("Lead Type must be provided.");

            var custCd = _userHelperService.GetCustCd();

            var result = await _legacyDeletedService.GetListAsync(leadType, custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPut("{leadType}")]
        public async Task<IActionResult> UpdateAsync(char leadType)
        {
            if (string.IsNullOrEmpty(leadType.ToString()))
                return BadRequest("Lead Type must be provided.");

            var custCd = _userHelperService.GetCustCd();

            await _legacyDeletedService.UpdateAsync(leadType, custCd);

            return Ok();
        }
    }
}
