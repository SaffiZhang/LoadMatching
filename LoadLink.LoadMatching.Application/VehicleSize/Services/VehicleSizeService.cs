using AutoMapper;
using LoadLink.LoadMatching.Domain.Caching;
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
        private readonly ICacheRepository<IEnumerable<GetVehicleSizeQuery>> _vehicleSizeCache;

        public VehicleSizeService(IVehicleSizeRepository vehicleSizeRepository, 
                                    IMapper mapper,
                                    ICacheRepository<IEnumerable<GetVehicleSizeQuery>> vehicleSizeCache)
        {
            _vehicleSizeRepository = vehicleSizeRepository;
            _mapper = mapper;
            _vehicleSizeCache = vehicleSizeCache;
        }

        public async Task<IEnumerable<GetVehicleSizeQuery>> GetListAsync()
        {
            var result = await _vehicleSizeCache.GetSingle($"VehicleSizes", async () =>
            {
                var data = await _vehicleSizeRepository.GetListAsync();
                if (!data.Any())
                    return null;

                return _mapper.Map<IEnumerable<GetVehicleSizeQuery>>(data);
            });

            return result;
        }

        public async Task<IEnumerable<GetVehicleSizeQuery>> GetListByPostTypeAsync(string postType)
        {
            var result = await _vehicleSizeCache.GetSingle($"VehicleSizesByPostType", async () =>
            {
                var data = await _vehicleSizeRepository.GetListByPostTypeAsync(postType);
                if (!data.Any())
                    return null;

                return _mapper.Map<IEnumerable<GetVehicleSizeQuery>>(data);
            });

            return result;
        }
    }
}
