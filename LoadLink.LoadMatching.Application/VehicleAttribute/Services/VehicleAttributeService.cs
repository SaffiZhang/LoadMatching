using AutoMapper;
using LoadLink.LoadMatching.Domain.Caching;
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
        private readonly ICacheRepository<IEnumerable<GetVehicleAttributeQuery>> _vehicleAttributeCache;

        public VehicleAttributeService(IVehicleAttributeRepository vehicleAttributeRepository, 
                                        IMapper mapper,
                                        ICacheRepository<IEnumerable<GetVehicleAttributeQuery>> vehicleAttributeCache)
        {
            _vehicleAttributeRepository = vehicleAttributeRepository;
            _mapper = mapper;
            _vehicleAttributeCache = vehicleAttributeCache;
        }

        public async Task<IEnumerable<GetVehicleAttributeQuery>> GetListAsync()
        {
            var result = await _vehicleAttributeCache.GetSingle($"VehicleAttributes", async () =>
            {
                var data = await _vehicleAttributeRepository.GetListAsync();
                if (!data.Any())
                    return null;

                return _mapper.Map<IEnumerable<GetVehicleAttributeQuery>>(data);
            });

            return result;
        }
    }
}
