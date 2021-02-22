using AutoMapper;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Application.NetworkMembers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Api.Controllers
{
    [Route("api/networks/{networkId}/members")]
    [ApiController]
    public class NetworkMembersController : ControllerBase
    {
        private readonly INetworkMembersService _networkMembersService;
        private readonly IUserHelperService _userHelperService;
        private readonly IMapper _mapper;

        public NetworkMembersController(
            INetworkMembersService networksService,
            IUserHelperService userHelperService,
            IMapper mapper)
        {
            _networkMembersService = networksService;
            _userHelperService = userHelperService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            if (id <= 0)
                return BadRequest();

            var result = await _networkMembersService.Get(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int networkId)
        {
            var custCd = _userHelperService.GetCustCd();

            if (networkId != 0)
            {

                var result = await _networkMembersService.GetList(networkId, custCd);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            else {

                var result = await _networkMembersService.GetList(custCd);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateNetworkMembersCommand createCommand)
        {
            if (createCommand == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _networkMembersService.Create(createCommand);
            if (result == null)
                return NoContent();

            return Ok(result);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int networkId, string id)
        {
            if (networkId == 0 || String.IsNullOrEmpty(id))
                return BadRequest();

            await _networkMembersService.Delete(networkId, id);

            return NoContent();
        }
    }
}
