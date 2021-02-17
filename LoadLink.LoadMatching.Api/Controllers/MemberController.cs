
using System.Threading.Tasks;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Application.Member.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IUserHelperService _userHelperService;

        public MemberController(IMemberService memberService,
                                IUserHelperService userHelperService)
        {
            _memberService = memberService;
            _userHelperService = userHelperService;
        }

        [HttpGet("{custCd}/{memberCustCd}")]
        public async Task<IActionResult> GetAsync(string custCd, string memberCustCd)
        {
            if (string.IsNullOrEmpty(custCd))
                custCd = _userHelperService.GetCustCd();

            var result = await _memberService.GetAsync(custCd, memberCustCd);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMemberCommand createMemberCommand)
        {
            if (createMemberCommand == null)
                return BadRequest();

            if (string.IsNullOrEmpty(createMemberCommand.CustCd))
                createMemberCommand.CustCd = _userHelperService.GetCustCd();

            var result = await _memberService.CreateAsync(createMemberCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateMemberCommand updateMemberCommand)
        {
            if (updateMemberCommand == null)
                return BadRequest();

            await _memberService.UpdateAsync(updateMemberCommand);

            return NoContent();
        }

        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteAsync(int memberId)
        {
            if (memberId <= 0)
                return BadRequest("Invalid MemberId!");

            await _memberService.DeleteAsync(memberId);

            return NoContent();
        }
    }
}
