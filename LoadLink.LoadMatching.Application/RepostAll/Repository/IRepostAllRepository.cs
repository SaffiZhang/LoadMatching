using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.RepostAll.Repository
{
    public interface IRepostAllRepository
    {
        Task<int> RepostAllAsync(string custCd, int userId);
    }
}
