using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.DATAccount.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatAccountController : ControllerBase
    {
        private readonly IDatAccountService _datAccountService;
        private readonly IUserHelperService _userHelperService;

        public DatAccountController(IDatAccountService datAccountService,
                                    IUserHelperService userHelperService)
        {
            _datAccountService = datAccountService;
            _userHelperService = userHelperService;
        }

        [HttpPost("{custcd}/{APIkey}")]
        public async Task<IActionResult> GetAsync(string custcd, string APIkey)
        {
            if (string.IsNullOrEmpty(custcd))
            {
                return BadRequest("Invalid CustCd.");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check DAT feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _datAccountService.GetAsync(custcd);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}

