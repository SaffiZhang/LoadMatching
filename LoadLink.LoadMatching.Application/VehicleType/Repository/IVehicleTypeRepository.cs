using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleType.Repository
{
    public interface IVehicleTypeRepository
    {
        Task<IEnumerable<UspGetVehicleTypeResult>> GetListAsync();
    }
}
