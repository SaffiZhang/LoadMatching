using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.VehicleSize.Services
{
    public interface IVehicleAttributeService
    {
        Task<IEnumerable<GetVehicleAttributeQuery>> GetListAsync();
    }
}
