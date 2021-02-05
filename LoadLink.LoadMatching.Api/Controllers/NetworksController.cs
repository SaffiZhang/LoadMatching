using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using System.Dynamic;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworksController : ControllerBase
    {
        private readonly INetworksService _networksService;
        private readonly IUserHelperService _userHelperService;
        private readonly IMapper _mapper;

        public NetworksController(
            INetworksService networksService,
            IUserHelperService userHelperService,
            IMapper mapper)
        {
            _networksService = networksService;
            _userHelperService = userHelperService;
            _mapper = mapper;
        }

        [HttpGet("get-networks")]
        public async Task<IActionResult> GetNetworksAsync(int networksId)
        {
            var result = await _networksService.GetAsync(networksId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("get-networks-list")]
        public async Task<IActionResult> GetNetworksListAsync()
        {
            var custCd = _userHelperService.GetCustCd();
            var userId = _userHelperService.GetUserId();

            var result = await _networksService.GetListAsync(custCd, userId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("create-networks")]
        public async Task<IActionResult> CreateNetworksAsync([FromBody] NetworksCommand networks)
        {
            if (networks == null)
            {
                return BadRequest();
            }

            networks.CustCD = _userHelperService.GetCustCd();
            networks.UserId = _userHelperService.GetUserId();

            var result = await _networksService.CreateAsync(networks);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPut("update-networks")]
        public async Task<IActionResult> UpdateNetworksAsync(int networksId, [FromBody] PatchNetworksCommand networksPatch)
        {
            if (networksPatch == null)
            {
                return BadRequest();
            }

            await _networksService.UpdateAsync(networksId, networksPatch.Name);
            
            return Ok();
        }

        [HttpPatch("patch-networks")]
        public async Task<IActionResult> PatchNetworksAsync(int networksId, [FromBody] JsonPatchDocument<PatchNetworksCommand> networksPatch)
        {
            if (networksPatch == null || networksPatch.Operations.Count == 0)
            {
                return BadRequest();
            }
            
            var value = (PatchNetworksCommand)networksPatch.Operations[0].value;

            await _networksService.UpdateAsync(networksId, value.Name);

            return Ok();
        }

        [HttpDelete("delete-networks")]
        public async Task<IActionResult> DeleteNetworksAsync(int networkId)
        {
            await _networksService.DeleteAsync(networkId);

            return Ok();
        }
    }
}
