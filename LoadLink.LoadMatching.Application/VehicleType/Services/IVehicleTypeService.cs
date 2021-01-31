using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.VehicleType.Services
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<GetVehicleTypesQuery>> GetListAsync();
    }
}
