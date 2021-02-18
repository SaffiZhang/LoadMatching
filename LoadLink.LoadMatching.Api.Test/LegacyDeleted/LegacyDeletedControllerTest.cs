using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.LegacyDeleted.Models.Queries;
using LoadLink.LoadMatching.Application.LegacyDeleted.Profiles;
using LoadLink.LoadMatching.Application.LegacyDeleted.Services;
using LoadLink.LoadMatching.Persistence.Repositories.LegacyDeleted;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.LegacyDeleted
{
    public class LegacyDeletedControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly ILegacyDeletedService _service;
        private readonly LegacyDeletedController _controller;

        public LegacyDeletedControllerTest()
        {
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(0, custCd);

            var legacyDeletedProfile = new LegacyDeletedProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(legacyDeletedProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new LegacyDeletedRepository(new DatabaseFixture().ConnectionFactory);
            _service = new LegacyDeletedService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object);
            _controller = new LegacyDeletedController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListAsync()
        {
            //arrange
            char leadType = 'B';

            // act
            var actionResult = await _controller.GetListAsync(leadType);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetUserLegacyDeletedQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task UpdateAsync()
        {
            //arrange
            char leadType = 'B';

            // act
            var actionResult = await _controller.UpdateAsync(leadType);
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
