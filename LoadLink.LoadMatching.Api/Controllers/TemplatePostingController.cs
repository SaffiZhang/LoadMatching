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

        [HttpGet("{templateid}")]
        public async Task<IActionResult> GetAsync(int templateId)
        {
           var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetAsync(custCd, templateId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var custCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.GetListAsync(custCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTemplatePostingCommand templatePosting)
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

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTemplatePostingCommand templatePosting)
        {
            if (templatePosting == null)
            {
                return BadRequest();
            }

            templatePosting.UpdatedBy = _userHelperService.GetUserId();
            templatePosting.UserId = _userHelperService.GetUserId();
            templatePosting.CustCd = _userHelperService.GetCustCd();

            var result = await _templatePostingService.UpdateAsync(templatePosting);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpDelete("{templateid}")]
        public async Task<IActionResult> DeleteAsync(int templateid)
        {
            await _templatePostingService.DeleteAsync(templateid, _userHelperService.GetUserId());

            return Ok();
        }
    }
}
