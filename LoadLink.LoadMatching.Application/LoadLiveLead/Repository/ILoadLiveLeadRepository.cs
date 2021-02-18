using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLiveLead.Repository
{
    public interface ILoadLiveLeadRepository
    {
        Task<IEnumerable<UspGetLoadLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId);
    }
}
