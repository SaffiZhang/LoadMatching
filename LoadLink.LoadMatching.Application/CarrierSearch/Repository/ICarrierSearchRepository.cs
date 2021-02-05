using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Repository
{
    public interface ICarrierSearchRepository
    {
        Task<IEnumerable<GetCarrierSearchResult>> GetCarrierSearch(GetCarrierSearchQuery searchrequest);
    }
}
