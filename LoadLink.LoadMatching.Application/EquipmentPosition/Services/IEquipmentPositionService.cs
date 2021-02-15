using LoadLink.LoadMatching.Application.EquipmentPosition.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.EquipmentPosition.Services
{
    public interface IEquipmentPositionService
    {
        Task<IEnumerable<GetEquipmentPositionQuery>> GetListAsync(int token);
        Task CreateAsync(int token);
    }
}
