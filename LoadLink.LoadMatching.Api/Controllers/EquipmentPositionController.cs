using System;
using System.Linq;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.EquipmentPosition.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentPositionController : ControllerBase
    {
        private readonly IEquipmentPositionService _equipmentPositionService;
        private readonly IUserHelperService _userHelperService;

        public EquipmentPositionController(IEquipmentPositionService equipmentPositionService,
                                        IUserHelperService userHelperService)
        {
            _equipmentPositionService = equipmentPositionService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{token}/{APIkey}")]
        public async Task<IActionResult> GetAsync(int token, string APIkey)
        {
            if (token == 0)
            {
                return BadRequest("Invalid Equipment Token.");
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            var result = await _equipmentPositionService.GetListAsync(token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("{token}/{APIkey}")]
        public async Task<IActionResult> CreateAsync(int token, string APIkey)
        {
            if (token == 0)
            {
                return BadRequest("Invalid Load Token."); // uses CreateLoad SP, so should be expecting Load Token
            }

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey.ToUpper()))
                return Ok(ResponseCode.NotSubscribe);

            await _equipmentPositionService.CreateAsync(token);

            return Ok();
        }
    }
}

