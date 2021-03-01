using LoadLink.LoadMatching.Application.MemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.MemberSearch.Services
{
    public interface IMemberSearchService
    {
        Task<IEnumerable<GetMemberSearchResult>> GetMemberSearch(GetMemberSearchRequest searchrequest,
                                                                MemberSearchSubscriptionsStatus subscriptions);
    }
}
