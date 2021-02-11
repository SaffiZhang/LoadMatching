
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using LoadLink.LoadMatching.Application.Contacted.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactedController : ControllerBase
    {
        private readonly IContactedService _contactedService;
        private readonly IUserHelperService _userHelperService;

        public ContactedController(IContactedService contactedService,
                                    IUserHelperService userHelperService)
        {
            _contactedService = contactedService;
            _userHelperService = userHelperService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateContactedCommand updateContactedCommand)
        {
            if (updateContactedCommand == null)
                return BadRequest();

            updateContactedCommand.UserId = _userHelperService.GetUserId();

            await _contactedService.UpdateAsync(updateContactedCommand);

            return Ok();
        }

    }
}
