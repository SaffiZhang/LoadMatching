using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LoadPosition.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadPositionController : ControllerBase
    {
        private readonly ILoadPositionService _loadPositionService;
        private readonly IUserHelperService _userHelperService;

        public LoadPositionController(ILoadPositionService loadPositionService,
                                        IUserHelperService userHelperService)
        {
            _loadPositionService = loadPositionService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{token}/{APIkey}")]
        public async Task<IActionResult> GetAsync(int token, string APIkey)
        {
            if (token == 0)
            {
                return BadRequest("Invalid Load Token.");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var result = await _loadPositionService.GetListAsync(token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("{token}/{APIkey}")]
        public async Task<IActionResult> CreateAsync(int token, string APIkey)
        {
            if (token == 0)
            {
                return BadRequest("Invalid Load Token.");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            await _loadPositionService.CreateAsync(token);

            return Ok();
        }
    }
}

