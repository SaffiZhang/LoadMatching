
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosition.Repository
{
    public interface IEquipmentPositionRepository
    {
        Task<IEnumerable<UspGetEquipmentPositionResult>> GetListAsync(int token);
        Task CreateAsync(int token);
    }
}
