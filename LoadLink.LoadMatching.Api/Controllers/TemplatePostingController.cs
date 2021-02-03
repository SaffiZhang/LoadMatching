using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using LoadLink.LoadMatching.Api.Infrastructure.Http;
using System;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatePostingController : ControllerBase
    {
        private readonly ITemplatePostingService _templatePostingService;
        private readonly IUserHelperService _userHelperService;

        public TemplatePostingController(
            ITemplatePostingService TemplatePostingService,
            IUserHelperService userHelperService)
        {
            _templatePostingService = TemplatePostingService;
            _userHelperService = userHelperService;
        }

        [HttpGet("get-template-posting")]
        public async Task<IActionResult> GetTemplatePostingAsync(int templateId, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetAsync(custCd, templateId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("get-template-posting-list")]
        public async Task<IActionResult> GetTemplatePostingListAsync(string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetListAsync(custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("create-template-posting-list")]
        public async Task<IActionResult> CreateTemplatePostingAsync([FromBody] TemplatePostingCommand templatePosting, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            if (templatePosting == null)
            {
                return BadRequest();
            }

            templatePosting.CreatedBy = _userHelperService.GetUserId();

            var result = await _templatePostingService.CreateAsync(templatePosting);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPut("update-template-posting-list")]
        public async Task<IActionResult> UpdateTemplatePostingAsync([FromBody] TemplatePostingCommand templatePosting, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            if (templatePosting == null)
            {
                return BadRequest();
            }

            templatePosting.UpdatedBy = _userHelperService.GetUserId();

            var result = await _templatePostingService.UpdateAsync(templatePosting);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpDelete("delete-template-posting-list")]
        public async Task<IActionResult> DeleteTemplatePostingAsync(int templateId, string apiKey)
        {
            if (!(await _userHelperService.HasValidSubscription(apiKey)))
                throw new UnauthorizedAccessException(ResponseCode.NotSubscribe.Message);

            await _templatePostingService.DeleteAsync(templateId, _userHelperService.GetUserId());

            return Ok();
        }
    }
}
