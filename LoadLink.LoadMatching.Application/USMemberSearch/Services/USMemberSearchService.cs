using AutoMapper;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USMemberSearch.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.USMemberSearch.Services
{
    public class USMemberSearchService : IUSMemberSearchService
    {
        private readonly IUSMemberSearchRepository _USMemberSearchRepository;
        private readonly IMapper _mapper;

        public USMemberSearchService(IUSMemberSearchRepository USMemberSearchRepository, IMapper mapper)
        {
            _USMemberSearchRepository = USMemberSearchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUSMemberSearchQuery>> GetListAsync(GetUSMemberSearchCommand searchRequest)
        {
            var result = await _USMemberSearchRepository.GetListAsync(searchRequest);
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetUSMemberSearchQuery>>(result);
        }
    }
}
