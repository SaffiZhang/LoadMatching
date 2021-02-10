using LoadLink.LoadMatching.Application.LeadCount.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LeadCount.Services
{
    public interface ILeadsCountService
    {
        Task<IEnumerable<GetLeadsCountQuery>> GetLeadsCountAsync(int userId, int token, bool getDAT, string type);
    }
}
