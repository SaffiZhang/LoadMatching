using AutoMapper;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Queries;
using LoadLink.LoadMatching.Application.NetworkMembers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.NetworkMembers.Services
{
    public class NetworkMembersService : INetworkMembersService
    {

        private readonly INetworkMembersRepository _networksMembersRepository;
        private readonly IMapper _mapper;

        public NetworkMembersService(INetworkMembersRepository networksMembersRepository, IMapper mapper)
        {
            _networksMembersRepository = networksMembersRepository;
            _mapper = mapper;
        }
        public async Task<CreateNetworkMembersCommand> Create(CreateNetworkMembersCommand createCommand)
        {
            return await _networksMembersRepository.Create(createCommand);
        }

        public async Task  Delete(int networkId, string custCd)
        {
            await _networksMembersRepository.Delete(networkId, custCd);
        }

        public async Task<GetNetworkMembersQuery> Get(int id)
        {
            var result = await _networksMembersRepository.Get(id);

            if (result == null)
                return null;

            return _mapper.Map<GetNetworkMembersQuery>(result);
        }

        public async Task<IEnumerable<GetNetworkMembersQuery>> GetList(string custCd)
        {
            var result = await _networksMembersRepository.GetList(custCd);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetNetworkMembersQuery>>(result);
        }

        public async Task<IEnumerable<GetNetworkMembersQuery>> GetList(int networkId, string custCd)
        {
            var result = await _networksMembersRepository.GetList(networkId, custCd);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetNetworkMembersQuery>>(result);
        }
    }
}
