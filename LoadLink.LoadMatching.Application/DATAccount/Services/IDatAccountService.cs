using LoadLink.LoadMatching.Application.DATAccount.Models.Queries;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATAccount.Services
{
    public interface IDatAccountService
    {
        Task<GetDatAccountQuery> GetAsync(string custCd);
    }
}
