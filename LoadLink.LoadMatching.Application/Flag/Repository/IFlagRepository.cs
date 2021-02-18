using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Flag.Repository
{
    public interface IFlagRepository
    {
        Task<UspGetFlagResult> GetAsync(string custCd, int Id);
        Task<IEnumerable<UspGetFlagResult>> GetListAsync(string custCd);
        Task<CreateFlagCommand> CreateAsync(CreateFlagCommand createFlagCommand);
        Task DeleteAsync(int id);
    }
}
