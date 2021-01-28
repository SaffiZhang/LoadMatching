using AutoMapper;
using LoadLink.LoadMatching.Application.VehicleAttribute.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleAttribute.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleAttribute.Services
{
    public class VehicleAttributeService : IVehicleAttributeService
    {
        private readonly IVehicleAttributeRepository _vehicleAttributeRepository;
        private readonly IMapper _mapper;

        public VehicleAttributeService(IVehicleAttributeRepository vehicleAttributeRepository, IMapper mapper)
        {
            _vehicleAttributeRepository = vehicleAttributeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetVehicleAttributeQuery>> GetListAsync()
        {
            var result = await _vehicleAttributeRepository.GetListAsync();
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetVehicleAttributeQuery>>(result);
        }
    }
}
