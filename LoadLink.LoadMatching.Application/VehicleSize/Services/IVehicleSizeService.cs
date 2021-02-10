using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.VehicleSize.Services
{
    public interface IVehicleSizeService
    {
        Task<IEnumerable<GetVehicleSizeQuery>> GetListAsync();
        Task<IEnumerable<GetVehicleSizeQuery>> GetListByPostTypeAsync(string postType);
    }
}
