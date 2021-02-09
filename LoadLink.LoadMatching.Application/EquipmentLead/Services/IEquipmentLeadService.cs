using LoadLink.LoadMatching.Application.EquipmentLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLead.Services
{
    public interface IEquipmentLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasDATSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }

        Task<IEnumerable<GetEquipmentLeadQuery>> GetListAsync(string custCD);
        Task<IEnumerable<GetEquipmentLeadQuery>> GetByPostingAsync(string custCD, int postingId);
        Task<IEnumerable<GetEquipmentLeadCombinedQuery>> GetCombinedAsync(string custCD, int postingId);
    }
}
