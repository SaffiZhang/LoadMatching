using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.DATAccount.Models.Queries;
using LoadLink.LoadMatching.Application.DATAccount.Profiles;
using LoadLink.LoadMatching.Application.DATAccount.Services;
using LoadLink.LoadMatching.Application.UserSubscription.Services;
using LoadLink.LoadMatching.Persistence.Repositories.DATAccount;
using LoadLink.LoadMatching.Persistence.Repositories.UserSubscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.DATAccount
{
    public class DatAccountControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IDatAccountService _service;
        private readonly DatAccountController _datAccountController;

        public DatAccountControllerTest()
        {
            var userId = 34344;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            //profile
            var datAccountProfile = new DatAccountProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(datAccountProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new DatAccountRepository(new DatabaseFixture().ConnectionFactory);
            _service = new DatAccountService(repository, profile);

            var userSubscriptionRepository = new UserSubscriptionRepository(new DatabaseFixture().ConnectionFactory);
            var mockCacheUserApiKey = new DatabaseFixture().MockCacheUserApiKey();

            _userSubscriptionService = new UserSubscriptionService(mockCacheUserApiKey.Object, userSubscriptionRepository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, _userSubscriptionService);
            _datAccountController = new DatAccountController(_service, _userHelper);
        }

        [Fact]
        public async Task GetAsync()
        {
            // arrange
            var LLB_DAT = "LLB_DAT";
            var custCd = "D000775";

            // act
            var actionResult = await _datAccountController.GetAsync(custCd, LLB_DAT);

            // assert
            var viewResult = Assert.IsType<ObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<GetDatAccountQuery>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
