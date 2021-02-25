using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Models.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Services
{
    public interface IDatEquipmentLiveLeadService
    {
        Task<IEnumerable<GetDatEquipmentLiveLeadQuery>> GetLeadsAsync(string custCd, string mileageProvider, 
                                                                        DateTime? leadFrom, int? postingId,
                                                                        DatEquipmentLiveLeadSubscriptionStatus subscriptions);
    }
}
