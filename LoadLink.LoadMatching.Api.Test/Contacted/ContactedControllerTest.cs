using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using LoadLink.LoadMatching.Application.Contacted.Services;
using LoadLink.LoadMatching.Persistence.Repositories.Contacted;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.Contacted
{
    public class ContactedControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IContactedService _service;
        private readonly ContactedController _contactedController;

        public ContactedControllerTest()
        {
            var userId = 34624;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            // integration            
            var repository = new ContactedRepository(new DatabaseFixture().ConnectionFactory);
            _service = new ContactedService(repository);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object);
            _contactedController = new ContactedController(_service, _userHelper);
        }

        [Fact]
        public async Task GetAsync()
        {
            // arrange
            var updateCommand = new UpdateContactedCommand
            {
                CnCustCd = "BCWFORC",
                EToken = 0,
                LToken = 9307984
            };

            // act
            var actionResult = await _contactedController.UpdateAsync(updateCommand);

            // assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
