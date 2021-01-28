using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.USMemberSearch.Repository
{
    public interface IUSMemberSearchRepository
    {
        Task<IEnumerable<UspGetUSMembersResult>> GetListAsync(GetUSMemberSearchCommand searchRequest);
    }
}
