using LoadLink.LoadMatching.Application.CarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Services
{
    public interface ICarrierSearchService
    {
        Task<IEnumerable<GetCarrierSearchResult>> GetCarrierSearchAsync(GetCarrierSearchRequest searchrequest,
                                                                        CarrierSearchSubscriptionsStatus subscriptions);
    }
}
