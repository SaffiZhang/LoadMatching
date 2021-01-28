using AutoMapper;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleSize.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleSize.Services
{
    public class VehicleSizeService : IVehicleSizeService
    {
        private readonly IVehicleSizeRepository _vehicleSizeRepository;
        private readonly IMapper _mapper;

        public VehicleSizeService(IVehicleSizeRepository vehicleSizeRepository, IMapper mapper)
        {
            _vehicleSizeRepository = vehicleSizeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetVehicleSizeQuery>> GetListAsync()
        {
            var result = await _vehicleSizeRepository.GetListAsync();
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetVehicleSizeQuery>>(result);
        }
    }
}
