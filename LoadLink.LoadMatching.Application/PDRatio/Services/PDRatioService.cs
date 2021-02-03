using AutoMapper;
using LoadLink.LoadMatching.Application.PDRatio.Models.Commands;
using LoadLink.LoadMatching.Application.PDRatio.Models.Queries;
using LoadLink.LoadMatching.Application.PDRatio.Repository;
using LoadLink.LoadMatching.Application.Common;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.PDRatio.Services
{
    public class PDRatioService : IPDRatioService
    {
        private readonly IPDRatioRepository _pdRatioRepository;
        private readonly IMapper _mapper;

        public PDRatioService(IPDRatioRepository pdRatioRepository, IMapper mapper)
        {
            _pdRatioRepository = pdRatioRepository;
            _mapper = mapper;
        }

        public async Task<GetPDRatioQuery> GetAsync(GetPDRatioCommand requestLane)
        {
            requestLane.VehicleTypeConverted = CommonLM.VTypeStringToNum(requestLane.VehicleType);

            var result = await _pdRatioRepository.GetAsync(requestLane);

            if (result == null)
                return null;

            return _mapper.Map<GetPDRatioQuery>(result);
        }
    }
}
