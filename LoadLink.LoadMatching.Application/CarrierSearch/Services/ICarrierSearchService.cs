using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Services
{
    public interface ICarrierSearchService
    {
        bool HasEQSubscription { get; set; }
        bool HasTCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }
        Task<IEnumerable<GetCarrierSearchResult>> GetCarrierSearch(GetCarrierSearchRequest searchrequest);
    }
}
