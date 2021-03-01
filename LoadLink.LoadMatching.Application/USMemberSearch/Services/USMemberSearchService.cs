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

        public async Task<IEnumerable<GetUSMemberSearchQuery>> GetListAsync(GetUSMemberSearchCommand searchRequest,
                                                                            USMemberSearchSubscriptionsStatus subscriptions)
        {
            var result = await _USMemberSearchRepository.GetListAsync(searchRequest);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                    row.TCC = subscriptions.HasTCSubscription ? row.TCC : -1;
                    row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                });

            return _mapper.Map<IEnumerable<GetUSMemberSearchQuery>>(result);
        }
    }
}
