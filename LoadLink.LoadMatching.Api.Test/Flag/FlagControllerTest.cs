using AutoMapper;
using LoadLink.LoadMatching.Api.Controllers;
using LoadLink.LoadMatching.Api.Services;
using LoadLink.LoadMatching.Api.Test.Setup;
using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Application.Flag.Models.Queries;
using LoadLink.LoadMatching.Application.Flag.Profiles;
using LoadLink.LoadMatching.Application.Flag.Services;
using LoadLink.LoadMatching.Persistence.Repositories.Flag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace LoadLink.LoadMatching.Api.Test.Flag
{
    public class FlagControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _fakeHttpContextAccessor;
        private readonly IUserHelperService _userHelper;
        private readonly IFlagService _service;
        private readonly FlagController _controller;

        public FlagControllerTest()
        {
            var userId = 34318;
            var custCd = "ONEXLOA";
            _fakeHttpContextAccessor = new FakeContext().MockHttpContext(userId, custCd);

            var flagProfile = new FlagProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(flagProfile));
            var profile = new Mapper(configuration);

            // integration            
            var repository = new FlagRepository(new DatabaseFixture().ConnectionFactory);
            _service = new FlagService(repository, profile);

            // controller
            _userHelper = new UserHelperService(_fakeHttpContextAccessor.Object);
            _controller = new FlagController(_service, _userHelper);
        }

        [Fact]
        public async Task GetAsync()
        {
            //arrange
            int id = 207930;

            // act
            var actionResult = await _controller.GetAsync(id);
            var okResult = actionResult as OkObjectResult;

            // assert
            if (okResult == null)
            {
                Assert.IsType<NoContentResult>(actionResult);
            }
            else
            {
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<GetFlagQuery>(viewResult.Value);
                Assert.NotNull(model);
            }
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
                var model = Assert.IsAssignableFrom<IEnumerable<GetFlagQuery>>(viewResult.Value);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Create_Flag()
        {
            // arrange
            var createCmd = new CreateFlagCommand
            {
                ContactCustCD = "BCMAGMA",
                LToken = 9257574,
                EToken = 29858887,
                LSrcCity = "Smithville",
                LSrcSt = "ON",
                LDestCity = "Huger",
                LDestSt = "SC",
                LVSize = "T",
                LVType = "FSTOI",
                LPostedDate = DateTime.Parse("2020-09-01 18:40:44.45"),
                LPAttrib = "",
                LComment = "STEVE",
                PSrcCity = "Milton",
                PSrcSt = "ON",
                PDestCity = "Jacksonville",
                PDestSt = "FL",
                PVType = "FNI",
                PVSize = "U",
                PPostedDate = DateTime.Parse("2020-09-01 10:55:34.51"),
                PPAttrib = "ACT",
                PComment = "289-801-3585",
                PostType = "E"
            };

            // act
            var actionResult = await _controller.CreateAsync(createCmd);
            var okResult = actionResult as OkObjectResult;
            var conflictResult = actionResult as ConflictResult;

            // assert
            if (conflictResult != null)
                Assert.IsType<ConflictResult>(actionResult);
            else
            {
                if (okResult == null)
                    Assert.IsType<BadRequestObjectResult>(actionResult);
                else
                    Assert.IsType<OkObjectResult>(actionResult);
            }
        }

        [Fact]
        public async Task DeleteAsync()
        {
            //arrange
            int id = 207930;

            // act
            var actionResult = await _controller.DeleteAsync(id);

            // assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task GetListAsync_Benchmark()
        {
            // arrange
            var numberOfRequests = 100;

            // act
            var timer = Stopwatch.StartNew();
            for (int i = 0; i < numberOfRequests; i++)
            {
                await _controller.GetListAsync();
            }
            timer.Stop();

            // assert
            var actualResultInSeconds = timer.Elapsed.TotalSeconds;
            var lowestExpectedRangeInSeconds = 5.0D; // seconds
            var tooHighRangeInSeconds = 30.0D; // seconds 

            Assert.InRange(actualResultInSeconds, lowestExpectedRangeInSeconds, tooHighRangeInSeconds);
        }
    }
}
