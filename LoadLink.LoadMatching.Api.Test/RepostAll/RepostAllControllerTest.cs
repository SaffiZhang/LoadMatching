using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.RepostAll.Services;
using LoadLink.LoadMatching.Persistence.Repositories.RepostAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.RepostAll
{
    public class RepostAllControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IRepostAllService _service;
        private readonly RepostAllController _repostAllController;

        public RepostAllControllerTest()
        {
            var userId = 1234;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            // integration            
            var repository = new RepostAllRepository(new DatabaseFixture().ConnectionFactory);
            _service = new RepostAllService(repository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, null);
            _repostAllController = new RepostAllController(_service, _userHelper);
        }

        [Fact]
        public async Task RepostAllAsync()
        {

            // act
            var actionResult = await _repostAllController.RepostAllAsync();

            // assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
