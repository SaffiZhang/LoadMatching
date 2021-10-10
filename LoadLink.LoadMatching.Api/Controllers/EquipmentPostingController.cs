using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using LoadLink.LoadMatching.Application.EquipmentPosting.Commands;
using System.Threading;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentPostingController : ControllerBase
    {
    
        private readonly IUserHelperService _userHelperService;
  
        private readonly IMediator _mediator;
       

        public EquipmentPostingController(IUserHelperService userHelperService, IMediator mediator)
        {
            _userHelperService = userHelperService;
            _mediator = mediator;
          
        }

        [HttpPost("{APIkey}")]
        public async Task<IActionResult> Post([FromBody] CreatEquipmentPostingCommand posting, string APIkey)
        {
            //DATA VALIDATIONS
            if (posting == null)
                return BadRequest("No Posting Data provided");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!ValidateVehicleType(posting.VehicleType))
                return BadRequest("Invalid VehicleType");
            if (!ValidatePostingAttrib(posting.PostingAttrib))
                return BadRequest("Invalid PostingAttrib");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            posting.CustCD = _userHelperService.GetCustCd(); 
            posting.CreatedBy = _userHelperService.GetUserId();
            
            try
            {
                var commandResult=  await _mediator.Send(posting);
                if (commandResult == null)
                {
                    return BadRequest();
                }

                return Ok(commandResult);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
              
        }

      
        private bool ValidateVehicleType(string VehicleType)
        {
            bool res = false;
            int count = 0;
            string pattern = @"[VvRrKkFfSsDdTtCcUuHhLlOoNnPpIiEe]";

            foreach (Match m in Regex.Matches(VehicleType, pattern))
            {
                count = count + 1;
            }

            if (VehicleType.Length == count)
                res = true;

            return res;
        }

        private bool ValidatePostingAttrib(string VehicleAttr)
        {
            bool res = false;
            int count = 0;
            string pattern = @"[VvFfTtCcHhNnIiEeAaBbWwZzXxMmGg]";

            if (VehicleAttr.Trim().Length > 0)
            {
                foreach (Match m in Regex.Matches(VehicleAttr, pattern))
                {
                    count = count + 1;
                }

                if (VehicleAttr.Length == count)
                    res = true;
            }
            else
            {
                res = true;
            }

            return res;
        }

    }

}
