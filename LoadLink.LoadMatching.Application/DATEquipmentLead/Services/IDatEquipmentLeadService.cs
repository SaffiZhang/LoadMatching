using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Services
{
    public interface IDatEquipmentLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncList(string custCd, string mileageProvider);
        Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncByPosting(string custCd, string mileageProvider, int postingId);

    }
}
