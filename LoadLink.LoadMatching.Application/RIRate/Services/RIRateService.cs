using AutoMapper;
using LoadLink.LoadMatching.Application.RIRate.Models.Commands;
using LoadLink.LoadMatching.Application.RIRate.Models.Queries;
using LoadLink.LoadMatching.Application.RIRate.Repository;
using LoadLink.LoadMatching.Application.Common;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.RIRate.Services
{
    public class RIRateService : IRIRateService
    {
        private readonly IRIRateRepository _riRateRepository;
        private readonly IMapper _mapper;

        public RIRateService(IRIRateRepository vehicleTypeRepository, IMapper mapper)
        {
            _riRateRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetRIRateQuery> GetAsync(GetRIRateCommand requestLane)
        {
            requestLane.VehicleTypeConverted = CommonLM.VTypeStringToNum(requestLane.VehicleType);

            var result = await _riRateRepository.GetAsync(requestLane);

            if (result == null)
                return null;

            return _mapper.Map<GetRIRateQuery>(result);
        }
    }
}
