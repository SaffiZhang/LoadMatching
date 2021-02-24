using LoadLink.LoadMatching.Application.EquipmentLead.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentLead.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLead.Services
{
    public interface IEquipmentLeadService
    {
        Task<IEnumerable<GetEquipmentLeadQuery>> GetListAsync(string custCD, string mileageProvider,
                                                                EquipmentLeadSubscriptionsStatus subscriptions);
        Task<IEnumerable<GetEquipmentLeadQuery>> GetByPostingAsync(string custCD, int postingId, string mileageProvider,
                                                                    EquipmentLeadSubscriptionsStatus subscriptions);
        Task<IEnumerable<GetEquipmentLeadCombinedQuery>> GetCombinedAsync(string custCD, int postingId, string mileageProvider, int leadsCap,
                                                                            EquipmentLeadSubscriptionsStatus subscriptions);
    }
}
