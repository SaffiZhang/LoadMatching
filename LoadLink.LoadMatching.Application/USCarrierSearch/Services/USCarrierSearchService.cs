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

        public async Task<IEnumerable<GetUSCarrierSearchQuery>> GetListAsync(GetUSCarrierSearchCommand searchRequest,
                                                                                USCarrierSearchSubscriptionsStatus subscriptions)
        {
            var result = await _USCarrierSearchRepository.GetListAsync(searchRequest);
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

            return _mapper.Map<IEnumerable<GetUSCarrierSearchQuery>>(resultList);
        }
    }
}
