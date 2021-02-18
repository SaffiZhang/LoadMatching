using LoadLink.LoadMatching.Application.Exclude.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Exclude.Repository
{
    public interface IExcludeRepository
    {
        Task<IEnumerable<UspGetExcludeResult>> GetListAsync(string custCd);
        Task<CreateExcludeCommand> CreateAsync(CreateExcludeCommand createExcludeCommand);
        Task DeleteAsync(string custCd, string excludeCustCd);
    }
}
