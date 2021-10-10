using AutoMapper;
using LoadLink.LoadMatching.Domain.Caching;
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
        private readonly ICacheRepository<IEnumerable<GetVehicleTypesQuery>> _vehicleTypeCache;

        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository, 
                                    IMapper mapper,
                                    ICacheRepository<IEnumerable<GetVehicleTypesQuery>> vehicleTypeCache)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
            _vehicleTypeCache = vehicleTypeCache;
        }

        public async Task<IEnumerable<GetVehicleTypesQuery>> GetListAsync()
        {
            var result = await _vehicleTypeCache.GetSingle($"VehicleTypes", async () =>
            {
                var data = await _vehicleTypeRepository.GetListAsync();
                if (!data.Any())
                    return null;

                return _mapper.Map<IEnumerable<GetVehicleTypesQuery>>(data);
            });

            return result;
        }
    }
}
