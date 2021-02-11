using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Repository
{
    public interface IDatEquipmentLeadRepository
    {
       Task <IEnumerable<UspGetDatEquipmentLiveLeadResult>> GetList(string custCd, string mileageProvider);
       Task <IEnumerable<UspGetDatEquipmentLiveLeadResult>> GetByPosting(string custCd, string mileageProvider , int postingId);
    }
}
