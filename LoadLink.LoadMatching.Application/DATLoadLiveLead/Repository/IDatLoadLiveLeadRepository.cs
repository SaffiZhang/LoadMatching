using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLiveLead.Repository
{
    public interface IDatLoadLiveLeadRepository
    {
        Task<IEnumerable<UspGetDatLoadLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);
    }
}
