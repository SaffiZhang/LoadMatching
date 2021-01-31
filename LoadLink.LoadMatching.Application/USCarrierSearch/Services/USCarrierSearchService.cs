using AutoMapper;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USCarrierSearch.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.USCarrierSearch.Services
{
    public class USCarrierSearchService : IUSCarrierSearchService
    {
        private readonly IUSCarrierSearchRepository _USCarrierSearchRepository;
        private readonly IMapper _mapper;

        public USCarrierSearchService(IUSCarrierSearchRepository USCarrierSearchRepository, IMapper mapper)
        {
            _USCarrierSearchRepository = USCarrierSearchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUSCarrierSearchQuery>> GetListAsync(GetUSCarrierSearchCommand searchRequest)
        {
            var result = await _USCarrierSearchRepository.GetListAsync(searchRequest);
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetUSCarrierSearchQuery>>(result);
        }
    }
}
