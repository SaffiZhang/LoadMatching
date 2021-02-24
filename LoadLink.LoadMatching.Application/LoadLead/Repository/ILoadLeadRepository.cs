using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadLead.Repository
{
    public interface ILoadLeadRepository
    {
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPostingAsync(string custCd, int postingID, string mileageProvider);
        Task<IEnumerable<UspGetLoadLeadResult>> GetListAsync(string custCd, string mileageProvider);
        Task<IEnumerable<UspGetLoadLeadResult>> GetByPosting_CombinedAsync(string custCd, int postingID, string mileageProvider, 
                                                                            int leadsCap, bool datStatus);
    }
}
