﻿using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentPostingController : ControllerBase
    {
        private readonly IEquipmentPostingService _equipmentPostingService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _appSettings;

        public EquipmentPostingController(IEquipmentPostingService equipmentPostingService, 
                                            IUserHelperService userHelperService, 
                                            IOptions<AppSettings> appSettings )
        {
            _equipmentPostingService = equipmentPostingService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{APIkey}")]
        public async Task<IActionResult> GetList(string APIkey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var postings = await _equipmentPostingService.GetListAsync(custCd, mileageProvider, false);

            if (postings == null)
            {
                return NoContent();
            }

            return Ok(postings);
        }

        [HttpGet("{APIkey}/GETDAT/{GETDAT?}")]
        public async Task<IActionResult> GetList(string APIkey, bool GETDAT)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var postings = await _equipmentPostingService.GetListAsync(custCd, mileageProvider, GETDAT);

            if (postings == null)
            {
                return NoContent();
            }

            return Ok(postings);
        }

        [HttpGet("{token}/{APIkey}")]
        public async Task<IActionResult> Get(int token, string APIkey)
        {
            if (token <= 0)
                return BadRequest("Invalid Equipment Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;

            var posting = await _equipmentPostingService.GetAsync(token, custCd, mileageProvider);

            if (posting == null)
            {
                return NoContent();
            }

            return Ok(posting);
        }

        [HttpPost("{APIkey}")]
        public async Task<IActionResult> Post([FromBody] CreateEquipmentPostingCommand posting, string APIkey)
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
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            posting.CustCD = _userHelperService.GetCustCd(); 
            posting.CreatedBy = _userHelperService.GetUserId();

            return Ok(await _equipmentPostingService.CreateAsync(posting));
        }

        [HttpPut("{token}/{APIkey}")]
        public async Task<IActionResult> Put(int token, [FromBody] UpdateEquipmentPostingCommand equipmentPosting, string APIkey)
        {
            if (equipmentPosting == null)
                return BadRequest("No Posting Status Provided");
            if (token <= 0)
                return BadRequest("Invalid Equipment Token");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            await _equipmentPostingService.UpdateAsync(token, equipmentPosting.PStatus);
            return NoContent();
        }
        
        [HttpPut("{token}")]
        public async Task<IActionResult> Put(int token, [FromBody] int InitialLeadsCount)
        {          
            if (token == 0)
                return BadRequest("Invalid Equipment Token");

            await _equipmentPostingService.UpdateLeadCount(token, InitialLeadsCount);
            return NoContent();           
        }

        [HttpPatch("{token}/{APIkey}")]
        public async Task<IActionResult> Patch(int token, [FromBody] JsonPatchDocument<UpdateEquipmentPostingCommand> patchDoc, string APIkey)
        {         
            //Ensure patch Doc is not null
            if (patchDoc == null)
                return BadRequest("No Posting Status Provided");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);
  
            //Get the Posting the update needs to be applied on
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
             
            var equipmentPostingToUpdate = await _equipmentPostingService.GetAsync(token, custCd, mileageProvider);
            //Ensure record to be updated exists
            if (equipmentPostingToUpdate == null)
                return NoContent();

            //apply update model
            var equipmentPostingToPatch = new UpdateEquipmentPostingCommand() { PStatus = equipmentPostingToUpdate.PStatus };
            patchDoc.ApplyTo(equipmentPostingToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _equipmentPostingService.UpdateAsync(token, equipmentPostingToPatch.PStatus);
            return NoContent();
        }

        [HttpDelete("{token}/{APIkey}")]
        public async Task<IActionResult> Delete(int token, string APIkey)
        {
            if (token <= 0)
                return BadRequest("Invalid Equipment Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            //Check if posting exsits before delete
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var userId = _userHelperService.GetUserId();
            var posting = await _equipmentPostingService.GetAsync(token, custCd, mileageProvider);

            if (posting == null)
            {
                return NoContent();
            }

            await  _equipmentPostingService.DeleteAsync(token, custCd, userId);
            return NoContent();
        }

        // VEHICLE TYPE & ATTRIBUTES VALIDATIONS
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
