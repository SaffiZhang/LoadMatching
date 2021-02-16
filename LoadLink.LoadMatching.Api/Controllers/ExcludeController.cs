using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.Exclude.Models.Commands;
using LoadLink.LoadMatching.Application.Exclude.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcludeController : ControllerBase
    {
        private readonly IExcludeService _excludeService;
        private readonly IUserHelperService _userHelperService;

        public ExcludeController(IExcludeService excludeService,
                                    IUserHelperService userHelperService)
        {
            _excludeService = excludeService;
            _userHelperService = userHelperService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _excludeService.GetListAsync(custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateExcludeCommand createExcludeCommand)
        {
            if (createExcludeCommand == null)
                return BadRequest();

            if (string.IsNullOrEmpty(createExcludeCommand.CustCD))
                createExcludeCommand.CustCD = _userHelperService.GetCustCd();

            var result = await _excludeService.CreateAsync(createExcludeCommand);

            return Ok(result);
        }

        [HttpDelete("{custCd}/{excludeCustCd}")]
        public async Task<IActionResult> DeleteAsync(string custCd, string excludeCustCd)
        {
            if (string.IsNullOrEmpty(excludeCustCd))
                return BadRequest("Invalid excludeCustCd!");

            if (string.IsNullOrEmpty(custCd))
                custCd = _userHelperService.GetCustCd();

            await _excludeService.DeleteAsync(custCd, excludeCustCd);

            return NoContent();
        }
    }
}
