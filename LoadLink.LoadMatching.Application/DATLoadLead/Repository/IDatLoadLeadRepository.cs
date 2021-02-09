using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATLoadLead.Repository
{
    public interface IDatLoadLeadRepository
    {
        Task<IEnumerable<UspGetDatLoadLeadResult>> GetListAsync(string custCD);
        Task<IEnumerable<UspGetDatLoadLeadResult>> GetByPostingAsync(string custCD, int postingId);
    }
}
