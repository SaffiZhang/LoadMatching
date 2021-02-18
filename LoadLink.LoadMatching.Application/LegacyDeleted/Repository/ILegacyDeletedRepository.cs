using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LegacyDeleted.Repository
{
    public interface ILegacyDeletedRepository
    {
        Task<IEnumerable<UspGetUserLegacyDeletedResult>> GetListAsync(char leadType, string custCd);
        Task UpdateAsync(char leadType, string custCd);
    }
}
