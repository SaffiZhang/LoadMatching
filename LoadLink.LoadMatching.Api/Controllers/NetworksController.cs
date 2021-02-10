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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _networksService.GetAsync(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var custCd = _userHelperService.GetCustCd();
            var userId = _userHelperService.GetUserId();

            var result = await _networksService.GetListAsync(custCd, userId);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NetworksCommand networks)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PatchNetworksCommand networksPatch)
        {
            if (networksPatch == null)
            {
                return BadRequest();
            }

            await _networksService.UpdateAsync(id, networksPatch.Name);
            
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<PatchNetworksCommand> networksPatch)
        {
            if (networksPatch == null || networksPatch.Operations.Count == 0)
            {
                return BadRequest();
            }
            
            var value = (PatchNetworksCommand)networksPatch.Operations[0].value;

            await _networksService.UpdateAsync(id, value.Name);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _networksService.DeleteAsync(id);

            return Ok();
        }
    }
}
