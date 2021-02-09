using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLead.Repository
{
    public interface ILoadLeadRepository
    {
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPostingAsync(string custCd, int postingID);
        Task<IEnumerable<UspGetLoadLeadResult>> GetListAsync(string custCd);
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPosting_CombinedAsync(string custCd, int postingID, bool datStatus);
    }
}
