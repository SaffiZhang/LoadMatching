using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.NetworkMembers.Repository
{
    public interface INetworkMembersRepository
    {
        Task<CreateNetworkMembersCommand> Create(CreateNetworkMembersCommand createCommand);
        Task<IEnumerable<UspGetNetworkMemberResult>> GetList(string custCd);
        Task<IEnumerable<UspGetNetworkMemberResult>> GetList(int networkId, string custCd);
        Task<UspGetNetworkMemberResult> Get(int id);
        Task Delete(int networkId, string custCd);
    }
}
