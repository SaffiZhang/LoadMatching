using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Networks.Services
{
    public interface INetworksService
    {
        Task<GetNetworksQuery> GetAsync(int networksId);
        Task<IEnumerable<GetNetworksQuery>> GetListAsync(string custCd, int userId);
        Task<NetworksCommand> CreateAsync(NetworksCommand networks);
        Task UpdateAsync(int networksId, string name);
        Task DeleteAsync(int networksId);
    }
}
