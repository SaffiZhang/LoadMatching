using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.MemberSearch.Repository
{
    public interface IMemberSearchRepository
    {
        Task<IEnumerable<UspGetMembersResult>> GetListAsync(GetMemberSearchQuery searchrequest);
    }
}
