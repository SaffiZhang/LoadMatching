using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.NetworkMembers.Services
{
    public interface INetworkMembersService
    {

        Task<CreateNetworkMembersCommand> Create(CreateNetworkMembersCommand createCommand);
        Task<IEnumerable<GetNetworkMembersQuery>> GetList(string custCd);
        Task<IEnumerable<GetNetworkMembersQuery>> GetList(int networkId, string custCd);
        Task<GetNetworkMembersQuery> Get(int id);
        Task Delete(int networkId, string custCd);
    }
}
