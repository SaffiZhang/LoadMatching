using LoadLink.LoadMatching.Application.DATLoadLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLead.Services
{
    public interface IDatLoadLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }

        Task<IEnumerable<GetDatLoadLeadQuery>> GetListAsync(string custCD);
        Task<IEnumerable<GetDatLoadLeadQuery>> GetByPostingAsync(string custCD, int postingId);
    }
}
