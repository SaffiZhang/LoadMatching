using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLiveLead.Repository
{
    public interface IEquipmentLiveLeadRepository
    {
        Task<IEnumerable<UspGetEquipmentLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);
    }
}
