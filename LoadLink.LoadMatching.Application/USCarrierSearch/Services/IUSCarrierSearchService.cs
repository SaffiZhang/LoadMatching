using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.USCarrierSearch.Services
{
    public interface IUSCarrierSearchService
    {
        Task<IEnumerable<GetUSCarrierSearchQuery>> GetListAsync(GetUSCarrierSearchCommand searchRequest);
    }
}
