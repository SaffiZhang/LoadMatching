using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLiveLead.Services
{
    public interface IEquipmentLiveLeadService
    {
        bool HasQPSubscription { get; set; }
        bool HasEQSubscription { get; set; }
        bool HasTCCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }


        Task<IEnumerable<GetEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);

    }
}
