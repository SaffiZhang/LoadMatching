using AutoMapper;
using LoadLink.LoadMatching.Application.VehicleType.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleType.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleType.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IMapper _mapper;

        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository, IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetVehicleTypesQuery>> GetListAsync()
        {
            var result = await _vehicleTypeRepository.GetListAsync();
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetVehicleTypesQuery>>(result);
        }
    }
}
