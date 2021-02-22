using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Services
{
    public interface IDatEquipmentLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetListAsync(string custCd);
        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncByPosting(string custCd, int postingId);

    }
}
