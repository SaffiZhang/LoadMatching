using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleSize.Repository
{
    public interface IVehicleAttributeRepository
    {
        Task<IEnumerable<UspGetVehicleSizeResult>> GetListAsync();
    }
}
