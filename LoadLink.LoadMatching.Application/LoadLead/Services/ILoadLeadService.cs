using LoadLink.LoadMatching.Application.LoadLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.LoadLead.Services
{
    public interface ILoadLeadService
    {
        bool HasDATStatusEnabled { get; set; }
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }
        Task<IEnumerable<GetLoadLeadQuery>> GetByPostingAsync(int postingID, string custCd, string mileageProvider);
        Task<IEnumerable<GetLoadLeadQuery>> GetListAsync(string custCd, string mileageProvider);
        Task<IEnumerable<GetLoadLeadQuery>> GetByPosting_CombinedAsync(int postingID, string custCd, string mileageProvider, int leadsCap);
    }
}
