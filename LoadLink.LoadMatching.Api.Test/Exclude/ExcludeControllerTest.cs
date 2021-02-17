using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.Exclude.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using LoadLink.LoadMatching.Application.Exclude.Profiles;
using LoadLink.LoadMatching.Persistence.Repositories.Exclude;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LoadLink.LoadMatching.Application.Exclude.Models.Queries;
using LoadLink.LoadMatching.Application.Exclude.Models.Commands;

namespace LoadLink.LoadMatching.Api.Test.Exclude
{
    public class ExcludeControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IExcludeService _service;
        private readonly ExcludeController _controller;

        public ExcludeControllerTest()
        {
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(0, custCd);

            var memberProfile = new ExcludeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(memberProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new ExcludeRepository(new DatabaseFixture().ConnectionFactory);
            _service = new ExcludeService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object);
            _controller = new ExcludeController(_service, _userHelper);
        }

        [Fact]
        public async Task GetListAsync()
        {
            // act
            var actionResult = await _controller.GetListAsync();
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetExcludeQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task CreateAsync()
        {
            // arrange
            var createCmd = new CreateExcludeCommand
            {
                CustCD = "TCORELL",
                ExcludeCustCD = "TESTQA1"
            };

            // act
            var actionResult = await _controller.CreateAsync(createCmd);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            //arrange
            var custCd = "TCORELL";
            var excludeCustCd = "TESTQA1";

            // act
            var actionResult = await _controller.DeleteAsync(custCd, excludeCustCd);
            Assert.IsType<NoContentResult>(actionResult);
        }
    }
}
