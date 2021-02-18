using LoadLink.LoadMatching.Application.LoadPosition.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosition.Services
{
    public interface ILoadPositionService
    {
        Task<IEnumerable<GetLoadPositionQuery>> GetListAsync(int token);
        Task CreateAsync(int token);
    }
}
