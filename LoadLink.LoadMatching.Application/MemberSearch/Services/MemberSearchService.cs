using AutoMapper;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.MemberSearch.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.MemberSearch.Services
{
    public class MemberSearchService : IMemberSearchService
    {
        private readonly IMemberSearchRepository _memberSearchRepository;
        private readonly IMapper _mapper;

        public MemberSearchService(IMemberSearchRepository memberSearchRepository, IMapper mapper)
        {
            _memberSearchRepository = memberSearchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetMemberSearchResult>> GetMemberSearch(GetMemberSearchRequest searchrequest,
                                                                                MemberSearchSubscriptionsStatus subscriptions)
        {
            var searchQuery = new GetMemberSearchQuery()
            {
               CompanyName = searchrequest.CompanyName,
               ProvSt = searchrequest.ProvSt,
               City = searchrequest.City,
               Phone = searchrequest.Phone,
               MemberType = searchrequest.MemberType == "" ? "All" : searchrequest.MemberType,
               GetLinkUS = searchrequest.GetLinkUS == "Y" ? 1 : 0,
               ShowExcluded = searchrequest.ShowExcluded,
               CustCd = searchrequest.CustCd
            };

            var result = await _memberSearchRepository.GetListAsync(searchQuery);
            if (!result.Any())
                return null;

            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row =>  {
                        row.Equifax = subscriptions.HasEQSubscription ? row.Equifax : -1;
                        row.TCC = subscriptions.HasTCSubscription ? row.TCC : -1;
                        row.TCUS = subscriptions.HasTCUSSubscription ? row.TCUS : -1;
                        });
           
            return _mapper.Map<IEnumerable<GetMemberSearchResult>>(resultList);              
        }
    }
}
