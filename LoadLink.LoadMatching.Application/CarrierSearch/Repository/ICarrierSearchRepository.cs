using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Repository
{
    public interface ICarrierSearchRepository
    {
        Task<IEnumerable<UspGetCarrierResult>> GetListAsync(GetCarrierSearchQuery searchrequest);
    }
}
