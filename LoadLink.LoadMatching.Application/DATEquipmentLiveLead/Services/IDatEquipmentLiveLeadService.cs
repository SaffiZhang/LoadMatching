using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Services
{
    public interface IDatEquipmentLiveLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetDatEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);

    }
}
