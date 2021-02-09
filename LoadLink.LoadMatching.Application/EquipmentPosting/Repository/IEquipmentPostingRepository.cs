using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Repository
{
    public interface IEquipmentPostingRepository
    {
        Task<int> CreateAsync(CreateEquipmentPostingCommand createCommand);
        Task<IEnumerable<UspGetEquipmentPostingResult>> GetListAsync(string custCd, string mileageProvider, bool? getDAT = false);

        Task<UspGetEquipmentPostingResult> GetAsync(int token, string custCd, string mileageProvider);

        Task UpdateAsync(int token, string pstatus);

        Task UpdateLeadCount(int token, int initialCount);
        Task DeleteAsync(int token, string custCd, int userId);
    }   
}
