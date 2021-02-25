using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Services
{
    public interface IDatEquipmentLeadService
    {
        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetListAsync(string custCd, DatEquipmentSubscriptionsStatus subscriptions, string mileageProvider);
        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncByPosting(string custCd, int postingId, DatEquipmentSubscriptionsStatus subscriptions, string mileageProvider);
    }
}
