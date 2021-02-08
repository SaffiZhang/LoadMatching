using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLead.Repository
{
    public interface ILoadLeadRepository
    {
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPostingAsync(int postingID, string custCd, string mileageProvider);
        Task<IEnumerable<UspGetLoadLeadResult>> GetListAsync(string custCd, string mileageProvider);
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPosting_CombinedAsync(int postingID, string custCd, string mileageProvider, bool datStatus, int leadsCap);
    }
}
