using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleAttribute.Repository
{
    public interface IVehicleAttributeRepository
    {
        Task<IEnumerable<UspGetVehicleAttributeResult>> GetListAsync();
    }
}
