using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.MemberSearch.Services
{
    public interface IMemberSearchService
    {
        bool HasEQSubscription { get; set; }
        bool HasTCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }
        Task<IEnumerable<GetMemberSearchResult>> GetMemberSearch(GetMemberSearchRequest searchrequest);
    }
}
