using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.VehicleAttribute.Services
{
    public interface IVehicleAttributeService
    {
        Task<IEnumerable<GetVehicleAttributeQuery>> GetListAsync();
    }
}
