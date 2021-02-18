using AutoMapper;
using LoadLink.LoadMatching.Application.LoadPosition.Models.Queries;
using LoadLink.LoadMatching.Application.LoadPosition.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosition.Services
{
    public class LoadPositionService : ILoadPositionService
    {
        private readonly ILoadPositionRepository _LoadPositionRepository;
        private readonly IMapper _mapper;

        public LoadPositionService(ILoadPositionRepository LoadPositionRepository, IMapper mapper)
        {
            _LoadPositionRepository = LoadPositionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetLoadPositionQuery>> GetListAsync(int token)
        {
            var result = await _LoadPositionRepository.GetListAsync(token);

            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetLoadPositionQuery>>(result);
        }

        public async Task CreateAsync(int token)
        {
            await _LoadPositionRepository.CreateAsync(token);
        }

    }
}
