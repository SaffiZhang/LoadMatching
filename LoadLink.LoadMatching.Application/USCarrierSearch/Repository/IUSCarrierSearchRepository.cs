using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.USCarrierSearch.Repository
{
    public interface IUSCarrierSearchRepository
    {
        Task<IEnumerable<UspGetUSCarrierResult>> GetListAsync(GetUSCarrierSearchCommand searchRequest);
    }
}
