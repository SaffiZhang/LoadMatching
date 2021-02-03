using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace LoadLink.LoadMatching.Api.Test.Setup
{
    public class FakeContext
    {
        public FakeContext()
        {

        }
        public Mock<IHttpContextAccessor> MockHttpContext(int userId)
        {
            return MockHttpContext(userId, string.Empty);
        }

        public Mock<IHttpContextAccessor> MockHttpContext(int userId, string custCd)
        {

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            // set user claims here
            var fakeIdentity = new GenericIdentity("User");
            fakeIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            fakeIdentity.AddClaim(new Claim("id", userId.ToString()));
            fakeIdentity.AddClaim(new Claim("cust_cd", custCd));
            var principal = new GenericPrincipal(fakeIdentity, null);

            // set header configuration here
            // ....


            mockHttpContextAccessor.Setup(t => t.HttpContext.User).Returns(principal);

            return mockHttpContextAccessor;

        }

        public Mock<ControllerContext> MockControllerContext(int userId)
        {
            var fakeHttpContext = MockHttpContext(userId);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object.HttpContext);

            return controllerContext;
        }
    }
}

