using LoadLink.LoadMatching.Application.Exclude.Models.Commands;
using LoadLink.LoadMatching.Application.Exclude.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Exclude.Services
{
    public interface IExcludeService
    {
        Task<IEnumerable<GetExcludeQuery>> GetListAsync(string custCd);
        Task<CreateExcludeCommand> CreateAsync(CreateExcludeCommand createExcludeCommand);
        Task DeleteAsync(string custCd, string excludeCustCd);
    }
}
