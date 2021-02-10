using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LeadCount.Repository
{
    public interface ILeadsCountRepository
    {
        Task<IEnumerable<UspGetLoadLeadsCountResult>> GetLoadLeadsCountAsync(int userId, int token, bool getDAT);
        Task<IEnumerable<UspGetEquipmentLeadsCountResult>> GetEquipLeadsCountAsync(int userId, int token, bool getDAT);
    }
}
