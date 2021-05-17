using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosting.Services
{
    public interface ILoadPostingService
    {
        Task<CreateLoadPostingCommand> CreateAsync(CreateLoadPostingCommand createCommand);
        Task<IEnumerable<GetLoadPostingQuery>> GetListAsync(string custCd, string mileageProvider, int leadsCap, bool? getDAT = false);
        Task<IEnumerable<GetLoadPostingLLQuery>> GetListLLAsync(string custCd, string mileageProvider, int leadsCap, DateTime liveLeadTime, bool? getDAT = false);
        Task<GetLoadPostingQuery> GetAsync(int token, string custCd, string mileageProvider, int leadsCap);
        Task UpdateAsync(int token, string pstatus);
        Task UpdateLeadCount(int token, int initialCount);
        Task DeleteAsync(int token, string custCd, int userId);
    }
}
