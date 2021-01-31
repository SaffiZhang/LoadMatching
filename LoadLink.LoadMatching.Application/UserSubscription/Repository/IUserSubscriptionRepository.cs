using LoadLink.LoadMatching.Domain.AggregatesModel.UserSubscriptionAggregate;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.UserSubscription.Repository
{
    public interface IUserSubscriptionRepository
    {
        Task<UserApiKey> UserApiKeys(int userId);
    }
}
