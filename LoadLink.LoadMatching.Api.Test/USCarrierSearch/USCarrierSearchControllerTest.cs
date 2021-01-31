using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USCarrierSearch.Profiles;
using LoadLink.LoadMatching.Application.USCarrierSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.USCarrierSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.USCarrierSearch
{
    public class USCarrierSearchControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IUSCarrierSearchService _service;
        private readonly USCarrierSearchController _USCarrierSearchController;

        public USCarrierSearchControllerTest()
        {
            var userId = 1235;
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId);

            var USCarrierSearchProfile = new USCarrierSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(USCarrierSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USCarrierSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USCarrierSearchService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object, null);
            _USCarrierSearchController = new USCarrierSearchController(_service, _userHelper);
        }

        [Fact]
        public async Task GetUSCarrierSearchListAsync()
        {
            // arrange
            var searchRequest = new GetUSCarrierSearchCommand
            {
                UserId = 1235,
                SrceSt = "",
                SrceCity = "",
                SrceRadius = 0,
                DestSt = "",
                DestCity = "",
                DestRadius = 0,
                VehicleType = 0,
                VehicleSize = 0,
                CompanyName = "",
                GetMexico = true
            };

            // act
            var actionResult = await _USCarrierSearchController.GetUSCarrierSearchAsync(searchRequest);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetUSCarrierSearchQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
