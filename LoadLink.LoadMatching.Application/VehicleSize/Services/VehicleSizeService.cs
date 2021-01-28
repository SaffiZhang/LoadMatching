using AutoMapper;
using LoadLink.LoadMatching.Application.VehicleSize.Models.Queries;
using LoadLink.LoadMatching.Application.VehicleSize.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.VehicleSize.Services
{
    public class VehicleAttributeService : IVehicleAttributeService
    {
        private readonly IVehicleAttributeRepository _vehicleSizeRepository;
        private readonly IMapper _mapper;

        public VehicleAttributeService(IVehicleAttributeRepository vehicleSizeRepository, IMapper mapper)
        {
            _vehicleSizeRepository = vehicleSizeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetVehicleAttributeQuery>> GetListAsync()
        {
            var result = await _vehicleSizeRepository.GetListAsync();
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetVehicleAttributeQuery>>(result);
        }
    }
}
