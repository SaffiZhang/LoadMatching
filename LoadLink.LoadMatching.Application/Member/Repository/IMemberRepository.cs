using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Member.Repository
{
    public interface IMemberRepository
    {
        Task<UspGetMemberResult> GetAsync(string custCd, string memberCustCd);
        Task<CreateMemberCommand> CreateAsync(CreateMemberCommand createMemberCommand);
        Task UpdateAsync(UpdateMemberCommand updateMemberCommand);
        Task DeleteAsync(int memberId);
    }
}
