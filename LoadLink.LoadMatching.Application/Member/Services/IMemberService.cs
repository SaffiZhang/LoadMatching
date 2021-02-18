using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Application.Member.Models.Queries;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Member.Services
{
    public interface IMemberService
    {
        Task<GetMemberQuery> GetAsync(string custCd, string memberCustCd);
        Task<CreateMemberCommand> CreateAsync(CreateMemberCommand createMemberCommand);
        Task UpdateAsync(UpdateMemberCommand updateMemberCommand);
        Task DeleteAsync(int memberId);
    }
}
