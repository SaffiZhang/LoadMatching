using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleSize.Repository
{
    public interface IVehicleSizeRepository
    {
        Task<IEnumerable<UspGetVehicleSizeResult>> GetListAsync();
        Task<IEnumerable<UspGetVehicleSizeResult>> GetListByPostTypeAsync(string postType);
    }
}
