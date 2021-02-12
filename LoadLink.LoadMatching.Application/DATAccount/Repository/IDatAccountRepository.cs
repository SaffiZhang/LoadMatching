using LoadLink.LoadMatching.Domain.Procedures;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATAccount.Repository
{
    public interface IDatAccountRepository
    {
        Task<UspGetDatAccountResult> GetAsync(string custCd);
    }
}
