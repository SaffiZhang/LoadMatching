using System.Threading.Tasks;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Services;
using LoadLink.LoadMatching.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworksController : ControllerBase
    {
        private readonly INetworksService _networksService;
        private readonly IUserHelperService _userHelperService;
        private readonly IMapper _mapper;

        public NetworksController(INetworksService networksService,
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
            if (id <= 0)
                return BadRequest();

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
                return BadRequest();

            if (!(networks.UserId.HasValue) || networks.UserId == 0)
                networks.UserId = _userHelperService.GetUserId();

            if (string.IsNullOrEmpty(networks.CustCD))
                networks.CustCD = _userHelperService.GetCustCd();
            
            var result = await _networksService.CreateAsync(networks);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PatchNetworksCommand networksPatch)
        {
            if (networksPatch == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Ensure record to be updated exists
            if (await _networksService.GetAsync(id) == null)
                return NoContent();

            await _networksService.UpdateAsync(id, networksPatch.Name);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<PatchNetworksCommand> networksPatch)
        {
            if (networksPatch == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Ensure record to be updated exists
            if (await _networksService.GetAsync(id) == null)
                return NoContent();

            var value = (PatchNetworksCommand)networksPatch.Operations[0].value;

            await _networksService.UpdateAsync(id, value.Name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            //Check if posting exsits before delete
            var network = await _networksService.GetAsync(id);
            if (network == null)
                return NoContent();

            await _networksService.DeleteAsync(id);
            return NoContent();
        }
    }
}
