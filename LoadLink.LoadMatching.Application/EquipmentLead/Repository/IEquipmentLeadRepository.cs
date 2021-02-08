using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentLead.Repository
{
    public interface IEquipmentLeadRepository
    {
        Task<IEnumerable<UspGetEquipmentLeadResult>> GetListAsync(string custCD);
        Task<IEnumerable<UspGetEquipmentLeadResult>> GetByPostingAsync(string custCD, int postingId);
        Task<IEnumerable<UspGetEquipmentLeadCombinedResult>> GetCombinedAsync(string custCD, int postingId, bool getDAT);
    }
}
