using LoadLink.LoadMatching.Application.LegacyDeleted.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LegacyDeleted.Services
{
    public interface ILegacyDeletedService
    {
        Task<IEnumerable<GetUserLegacyDeletedQuery>> GetListAsync(char leadType, string custCd);
        Task UpdateAsync(char leadType, string custCd);
    }
}
