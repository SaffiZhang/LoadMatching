using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosting.Repository
{
    public interface ILoadPostingRepository
    {
        Task<int> CreateAsync(UspCreateLoadPostingCommand createCommand);
        Task<IEnumerable<UspGetLoadPostingResult>> GetListAsync(string custCd, string mileageProvider, bool? getDAT = false);
        Task<IEnumerable<UspGetLoadPostingLLResult>> GetListLLAsync(string custCd, string mileageProvider, DateTime liveLeadTime, bool? getDAT = false);
        Task<UspGetLoadPostingResult> GetAsync(int token, string custCd, string mileageProvider);
        Task UpdateAsync(int token, string pstatus);
        Task UpdateLeadCount(int token, int initialCount);
        Task DeleteAsync(int token, string custCd, int userId);
    }
}
