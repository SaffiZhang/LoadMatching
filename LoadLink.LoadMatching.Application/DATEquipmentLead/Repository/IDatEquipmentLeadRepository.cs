using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Repository
{
    public interface IDatEquipmentLeadRepository
    {
       Task <IEnumerable<UspGetDatEquipmentLeadResult>> GetList(string custCd, string mileageProvider);
       Task <IEnumerable<UspGetDatEquipmentLeadResult>> GetByPosting(string custCd, int postingId, string mileageProvider);
    }
}
