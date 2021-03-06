using LoadLink.LoadMatching.Api.Configuration;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Application.LoadPosting.Services;
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
    public class LoadPostingController : ControllerBase
    {
        private readonly ILoadPostingService _loadPostingService;
        private readonly IUserHelperService _userHelperService;
        private readonly AppSettings _appSettings;

        public LoadPostingController(ILoadPostingService loadPostingService, 
                                        IUserHelperService userHelperService, 
                                        IOptions<AppSettings> appSettings)
        {
            _loadPostingService = loadPostingService;
            _userHelperService = userHelperService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{APIkey}")]
        public async Task<IActionResult> GetList(string APIkey)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;

            var postings = await _loadPostingService.GetListAsync(custCd, mileageProvider, leadsCap, false);

            if (postings == null)
                return NoContent();

            return Ok(postings);
        }

        [HttpGet("{APIkey}/GETDAT/{GETDAT?}")]
        public async Task<IActionResult> GetList(string APIkey, bool GETDAT)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;

            var postings = await _loadPostingService.GetListAsync(custCd, mileageProvider, leadsCap, GETDAT);

            if (postings == null)
                return NoContent();

            return Ok(postings);
        }

        [HttpGet("{APIkey}/GETDAT/{GETDAT}/{LiveLeadTime}")]
        public async Task<IActionResult> GetListLLAsync(string APIkey, bool GETDAT, DateTime LiveLeadTime)
        {
            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;

            var postings = await _loadPostingService.GetListLLAsync(custCd, mileageProvider, leadsCap, LiveLeadTime, GETDAT);

            if (postings == null)
                return NoContent();

            return Ok(postings);
        }

        [HttpGet("{token}/{APIkey}")]
        public async Task<IActionResult> Get(int token, string APIkey)
        {
            if (token <= 0)
                return BadRequest("Invalid Load Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Get the result
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;

            var posting = await _loadPostingService.GetAsync(token, custCd, mileageProvider, leadsCap);

            if (posting == null)
                return NoContent();

            return Ok(posting);
        }

        [HttpPost("{APIkey}")]
        public async Task<IActionResult> Post([FromBody] CreateLoadPostingCommand posting, string APIkey)
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

            return Ok(await _loadPostingService.CreateAsync(posting));
        }

        [HttpPut("{token}/{APIkey}")]
        public async Task<IActionResult> Put(int token, [FromBody] UpdateLoadPostingCommand loadPosting, string APIkey)
        {
            if (loadPosting == null)
                return BadRequest("No Posting Status Provided");
            if (token <= 0)
                return BadRequest("Invalid Load Token");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            await _loadPostingService.UpdateAsync(token, loadPosting.PStatus);

            return NoContent();
        }

        [HttpPut("{token}")]
        public async Task<IActionResult> Put(int token, [FromBody] int InitialLeadsCount)
        {
            if (token == 0)
                return BadRequest("Invalid Load Token");

            await _loadPostingService.UpdateLeadCount(token, InitialLeadsCount);

            return NoContent();
        }

        [HttpPatch("{token}/{APIkey}")]
        public async Task<IActionResult> Patch(int token, [FromBody] JsonPatchDocument<UpdateLoadPostingCommand> patchDoc, string APIkey)
        {
            //Ensure patch Doc is not null
            if (patchDoc == null)
                return BadRequest("No Posting Status Provided");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Get the Posting the update needs to be applied on
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;

            var loadPostingToUpdate = await _loadPostingService.GetAsync(token, custCd, mileageProvider, leadsCap);

            //Ensure record to be updated exists
            if (loadPostingToUpdate == null)
                return NoContent();

            //apply update model
            var loadPostingToPatch = new UpdateLoadPostingCommand() { PStatus = loadPostingToUpdate.PStatus };
            patchDoc.ApplyTo(loadPostingToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _loadPostingService.UpdateAsync(token, loadPostingToPatch.PStatus);

            return NoContent();
        }

        [HttpDelete("{token}/{APIkey}")]
        public async Task<IActionResult> Delete(int token, string APIkey)
        {
            if (token <= 0)
                return BadRequest("Invalid Load Token");

            var getUserApiKeys = await _userHelperService.GetUserApiKeys();

            // check feature access
            if (!getUserApiKeys.Contains(APIkey))
                return Ok(ResponseCode.NotSubscribe);

            //Check if posting exsits before delete
            var custCd = _userHelperService.GetCustCd();
            var mileageProvider = _appSettings.AppSetting.MileageProvider;
            var leadsCap = _appSettings.AppSetting.LeadsCap;
            var userId = _userHelperService.GetUserId();

            var posting = await _loadPostingService.GetAsync(token, custCd, mileageProvider, leadsCap);

            if (posting == null)
                return NoContent();

            //Open = O, Expired = E
            if (!(posting.PStatus.Equals("O") || posting.PStatus.Equals("E")))
            {
                return BadRequest("Invalid token status");
            }

            await _loadPostingService.DeleteAsync(token, custCd, userId);

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
