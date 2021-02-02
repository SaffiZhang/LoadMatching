using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedEquipment.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedEquipmentController : ControllerBase
    {
        private readonly IAssignedEquipmentService _assignedEquipmentService;
        private readonly IUserHelperService _userHelperService;

        public AssignedEquipmentController(IAssignedEquipmentService assignedEquipmentService,
                                            IUserHelperService userHelperService)
        {
            _assignedEquipmentService = assignedEquipmentService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAsync(int token)
        {
            var result = await _assignedEquipmentService.GetAsync(token);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAssignedEquipmentCommand updateAssignedEquipmentCommand)
        {
            if (updateAssignedEquipmentCommand == null)
            {
                return BadRequest();
            }

            updateAssignedEquipmentCommand.UpdatedBy = _userHelperService.GetUserId();

            var result = await _assignedEquipmentService.UpdateAsync(updateAssignedEquipmentCommand);

            return Ok(result);
        }
    }
}
