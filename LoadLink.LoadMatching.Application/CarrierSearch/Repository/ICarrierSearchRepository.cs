using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Repository
{
    public interface ICarrierSearchRepository
    {
        IEnumerable<GetCarrierSearchResult> GetCarrierSearch(GetCarrierSearchQuery searchrequest);
    }
}
