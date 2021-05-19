using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Services
{
    public interface IEquipmentPostingService
    {
        Task<CreateEquipmentPostingCommand> CreateAsync(CreateEquipmentPostingCommand createCommand);
        Task<IEnumerable<GetEquipmentPostingQuery>> GetListAsync(string custCd, string mileageProvider, int leadsCap, bool? getDAT = false);
        Task<IEnumerable<GetEquipmentPostingLLQuery>> GetListLLAsync(string custCd, string mileageProvider, int leadsCap, DateTime liveLeadTime, bool? getDAT = false);
        Task<GetEquipmentPostingQuery> GetAsync(int token, string custCd, string mileageProvider, int leadsCap);
        Task UpdateAsync(int token, string pstatus);
        Task UpdateLeadCount(int token, int initialCount);
        Task DeleteAsync(int token, string custCd, int userId);
    }
}
