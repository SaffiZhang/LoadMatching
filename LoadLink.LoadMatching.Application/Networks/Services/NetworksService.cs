using AutoMapper;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Models.Queries;
using LoadLink.LoadMatching.Application.Networks.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Networks.Services
{
    public class NetworksService : INetworksService
    {
        private readonly INetworksRepository _networksRepository;
        private readonly IMapper _mapper;

        public NetworksService(INetworksRepository networksRepository, IMapper mapper)
        {
            _networksRepository = networksRepository;
            _mapper = mapper;
        }

        public async Task<NetworksCommand> CreateAsync(NetworksCommand networks)
        {
            return await _networksRepository.CreateAsync(networks);
        }

        public async Task DeleteAsync(int networksId)
        {
            await _networksRepository.DeleteAsync(networksId);
        }

        public async Task<GetNetworksQuery> GetAsync(int networksId)
        {
            var result = await _networksRepository.GetAsync(networksId);

            if (result == null)
                return null;

            return _mapper.Map<GetNetworksQuery>(result);
        }

        public async Task<IEnumerable<GetNetworksQuery>> GetListAsync(string custCd, int userId)
        {
            var result = await _networksRepository.GetListAsync(custCd, userId);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetNetworksQuery>>(result);
        }

        public async Task UpdateAsync(int networksId, string name)
        {
            await _networksRepository.UpdateAsync(networksId, name);
        }
    }
}
