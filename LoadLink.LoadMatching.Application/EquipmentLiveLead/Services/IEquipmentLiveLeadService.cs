using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Models.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLiveLead.Services
{
    public interface IEquipmentLiveLeadService
    {
        Task<IEnumerable<GetEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, 
                                                                    DateTime? leadfrom, int? postingId,
                                                                    EquipmentLiveLeadSubscriptionsStatus subscriptions);
    }
}
