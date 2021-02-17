using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Application.Member.Models.Queries;
using LoadLink.LoadMatching.Application.Member.Profiles;
using LoadLink.LoadMatching.Application.Member.Services;
using LoadLink.LoadMatching.Persistence.Repositories.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.Member
{
    public class MemberControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IMemberService _service;
        private readonly MemberController _memberController;

        public MemberControllerTest()
        {
            var custCd = "TCORELL";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(0,custCd);

            var memberProfile = new MemberProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(memberProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new MemberRepository(new DatabaseFixture().ConnectionFactory);
            _service = new MemberService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object);
            _memberController = new MemberController(_service, _userHelper);
        }

        [Fact]
        public async Task GetAsync()
        {
            //arrange
            var custCd = "TCORELL";
            var memberCustCd = "TESTQA2";

            // act
            var actionResult = await _memberController.GetAsync(custCd, memberCustCd);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<GetMemberQuery>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task CreateAsync()
        {
            // arrange
            var createCmd = new CreateMemberCommand
            {
                CustCd = "TCORELL",
                MemberCustCd = "TESTQA1",
                DispatchNote = "test note create"
            };

            // act
            var actionResult = await _memberController.CreateAsync(createCmd);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // arrange
            var updateCmd = new UpdateMemberCommand
            {
                MemberId = 1534,
                DispatchNote = "test update note"
            };

            // act
            var actionResult = await _memberController.UpdateAsync(updateCmd);

            // assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            //arrange
            int memberId = 1485;

            // act
            var actionResult = await _memberController.DeleteAsync(memberId);
            Assert.IsType<NoContentResult>(actionResult);
        }
    }
}
