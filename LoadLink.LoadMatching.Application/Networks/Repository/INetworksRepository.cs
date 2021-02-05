using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Networks.Repository
{
    public interface INetworksRepository
    {
        Task<UspGetNetworkResult> GetAsync(int networksId);
        Task<IEnumerable<UspGetNetworkResult>> GetListAsync(string custCd, int userId);
        Task<NetworksCommand> CreateAsync(NetworksCommand networks);
        Task UpdateAsync(int networksId, string name);
        Task DeleteAsync(int networksId);
    }
}
