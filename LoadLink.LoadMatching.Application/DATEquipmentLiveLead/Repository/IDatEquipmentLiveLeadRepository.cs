using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Repository
{
    public interface IDatEquipmentLiveLeadRepository
    {
        Task<IEnumerable<UspGetDatEquipmentLiveLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);
    }
}
