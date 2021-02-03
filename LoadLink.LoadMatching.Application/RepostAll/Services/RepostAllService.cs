using LoadLink.LoadMatching.Application.RepostAll.Repository;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.RepostAll.Services
{
    public class RepostAllService : IRepostAllService
    {
        private readonly IRepostAllRepository _RepostAllRepository;

        public RepostAllService(IRepostAllRepository RepostAllRepository)
        {
            _RepostAllRepository = RepostAllRepository;
        }

        public async Task<int> RepostAllAsync(string custCd, int userId)
        {
            return await _RepostAllRepository.RepostAllAsync(custCd, userId);
        }
    }
}
