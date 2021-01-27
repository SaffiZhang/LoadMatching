using LoadLink.LoadMatching.Api.Test.Setup;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Models.Queries;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;

namespace LoadLink.LoadMatching.Api.Test.UserSubscription
{
    public class UserSubscriptionServiceTest
    {

        private readonly IUserSubscriptionService _service;

        public UserSubscriptionServiceTest()
        {
            // mock cache
            var cacheRepository = new DatabaseFixture().MockCacheUserApiKey();

            // integration            
            var repository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            _service = new UserSubscriptionService(cacheRepository.Object, repository);

        }


        [Fact]
        public async Task User_api_keys_should_have_values()
        {
            // arrange           
            int userId = 34368;

            // act
            var result = await _service.GetUserApiKeys(userId);

            // assert 
            Assert.NotNull(result);
            Assert.True(result.ApiKeys?.Count() > 0, $"There is a total of {result.ApiKeys?.Count()} data associated for userId {userId}");

        }


    }
}
