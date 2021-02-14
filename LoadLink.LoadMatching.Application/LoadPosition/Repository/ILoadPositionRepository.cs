
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosition.Repository
{
    public interface ILoadPositionRepository
    {
        Task<IEnumerable<UspGetLoadPositionResult>> GetListAsync(int token);
        Task CreateAsync(int token);
    }
}
