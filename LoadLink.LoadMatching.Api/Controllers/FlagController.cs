using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Application.Flag.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {
        private readonly IFlagService _flagService;
        private readonly IUserHelperService _userHelperService;

        public FlagController(IFlagService flagService,
                                IUserHelperService userHelperService)
        {
            _flagService = flagService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _flagService.GetAsync(custCd, id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _flagService.GetListAsync(custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFlagCommand createFlagCommand)
        {
            if (createFlagCommand == null)
                return BadRequest();

            var custCd = _userHelperService.GetCustCd();
            var userId = _userHelperService.GetUserId();

            createFlagCommand.CustCD = custCd;
            createFlagCommand.CreatedBy = userId;

            var result = await _flagService.CreateAsync(createFlagCommand);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Flag Id!");

            var custCd = _userHelperService.GetCustCd();

            var flag = _flagService.GetAsync(custCd, id);
            if (flag.Result == null)
                return NoContent();

            await _flagService.DeleteAsync(id);
            return NoContent();
        }
    }
}
