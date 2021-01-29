using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.USMemberSearch.Profiles;
using LoadLink.LoadMatching.Application.USMemberSearch.Services;
using LoadLink.LoadMatching.Persistence.Repositories.USMemberSearch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.USMemberSearch
{
    public class USCarrierSearchControllerTest
    {
        private readonly IUSMemberSearchService _service;
        private readonly USMemberSearchController _USMemberSearchController;

        public USCarrierSearchControllerTest()
        {
            var USMemberSearchProfile = new USMemberSearchProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(USMemberSearchProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new USMemberSearchRepository(new DatabaseFixture().ConnectionFactory);
            _service = new USMemberSearchService(repository, profile);

            // controller
            _USMemberSearchController = new USMemberSearchController(_service);
        }

        [Fact]
        public async Task GetUSMemberSearchListAsync()
        {
            // arrange
            var searchRequest = new GetUSMemberSearchCommand
            {
                CustCd = "TCORELL",
                CompanyName = "",
                Phone = "",
                ProvSt = "",
                ShowExcluded = SearchType.All
            };

            // act
            var actionResult = await _USMemberSearchController.GetUSMemberSearchAsync(searchRequest);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<GetUSMemberSearchQuery>>(viewResult.Value);
            Assert.NotNull(model);
        }
    }
}
