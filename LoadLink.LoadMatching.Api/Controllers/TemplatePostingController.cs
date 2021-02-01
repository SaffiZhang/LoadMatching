using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetTemplatePostingAsync(int templateId)
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetAsync(custCd, templateId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("get-template-posting-list")]
        public async Task<IActionResult> GetTemplatePostingListAsync()
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetListAsync(custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("create-template-posting-list")]
        public async Task<IActionResult> CreateTemplatePostingAsync([FromBody] TemplatePostingCommand templatePosting)
        {
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
        public async Task<IActionResult> UpdateTemplatePostingAsync([FromBody] TemplatePostingCommand templatePosting)
        {
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
        public async Task<IActionResult> DeleteTemplatePostingAsync(int templateId)
        {
            await _templatePostingService.DeleteAsync(templateId, _userHelperService.GetUserId());

            return Ok();
        }
    }
}
