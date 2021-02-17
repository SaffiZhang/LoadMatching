using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Application.Flag.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Flag.Services
{
    public interface IFlagService
    {
        Task<GetFlagQuery> GetAsync(string custCd, int Id);
        Task<IEnumerable<GetFlagQuery>> GetListAsync(string custCd);
        Task<CreateFlagCommand> CreateAsync(CreateFlagCommand createFlagCommand);
        Task DeleteAsync(int id);
    }
}
