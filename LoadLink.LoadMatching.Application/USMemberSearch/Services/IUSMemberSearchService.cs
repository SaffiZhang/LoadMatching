using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.USMemberSearch.Services
{
    public interface IUSMemberSearchService
    {
        bool HasEQSubscription { get; set; }
        bool HasTCSubscription { get; set; }
        bool HasTCUSSubscription { get; set; }
        Task<IEnumerable<GetUSMemberSearchQuery>> GetListAsync(GetUSMemberSearchCommand searchRequest);
    }
}
