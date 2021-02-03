using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.RepostAll.Services
{
    public interface IRepostAllService
    {
        Task<int> RepostAllAsync(string custCd, int userId);
    }
}
