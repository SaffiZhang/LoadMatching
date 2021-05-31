

using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedLoad.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedLoadController : ControllerBase
    {
        private readonly IAssignedLoadService _assignedLoadService;
        private readonly IUserHelperService _userHelperService;

        public AssignedLoadController(IAssignedLoadService assignedLoadService,
                                            IUserHelperService userHelperService)
        {
            _assignedLoadService = assignedLoadService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAsync(int token)
        {
            //Token could be 0 per the repository logic.
            var userId = _userHelperService.GetUserId();

            var result = await _assignedLoadService.GetAsync(token, userId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var userId = _userHelperService.GetUserId();

            var result = await _assignedLoadService.GetListAsync(userId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAssignedLoadCommand createAssignedLoadCommand)
        {
            if (createAssignedLoadCommand == null)
                return BadRequest();

            var userId = _userHelperService.GetUserId();

            //Check if assignedLoad already exists for this Token
            var posting = _assignedLoadService.GetAsync(createAssignedLoadCommand.Token, userId);
            if (posting.Result != null)
                return BadRequest($"AssignedLoad for Token { createAssignedLoadCommand.Token } already exists!");

            //Proceed to create if doesn't exist
            createAssignedLoadCommand.CreatedBy = userId;
            var result = await _assignedLoadService.CreateAsync(createAssignedLoadCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAssignedLoadCommand updateAssignedLoadCommand)
        {
            if (updateAssignedLoadCommand == null)
                return BadRequest();

            updateAssignedLoadCommand.UpdatedBy = _userHelperService.GetUserId();

            var result = await _assignedLoadService.UpdateAsync(updateAssignedLoadCommand);

            return Ok(result);
        }

        [HttpPut("update-customer-tracking/{APIkey}")]
        public async Task<IActionResult> UpdateCustomerTrackingAsync(
                [FromBody] UpdateCustomerTrackingCommand updateCustomerTrackingCommand, string APIkey)
        {
            if (!(await _userHelperService.HasValidSubscription(APIkey)))
                return Ok(ResponseCode.NotSubscribe);

            if (updateCustomerTrackingCommand == null)
                return BadRequest();

            updateCustomerTrackingCommand.UpdatedBy = _userHelperService.GetUserId();

            var result = await _assignedLoadService.UpdateCustomerTrackingAsync(updateCustomerTrackingCommand);

            return Ok(result);
        }

        [HttpDelete("{token}")]
        public async Task<IActionResult> DeleteAsync(int token)
        {
            if (token <= 0)
                return BadRequest("Invalid Token!");

            var userId = _userHelperService.GetUserId();

            //Check if posting exists before deleting
            var posting = _assignedLoadService.GetAsync(token, userId);
            if (posting.Result == null)
                return NoContent();

            var result = await _assignedLoadService.DeleteAsync(token, userId);

            return Ok(result);
        }
    }
}
