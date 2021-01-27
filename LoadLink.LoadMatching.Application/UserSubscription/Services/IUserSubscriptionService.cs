using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.UserSubscription.Services
{
    public interface IUserSubscriptionService
    {
        Task<UserApiKeyQuery> GetUserApiKeys(int userId);
        Task<bool> IsValidApiKey(int userId, string apiKey);
    }
}
