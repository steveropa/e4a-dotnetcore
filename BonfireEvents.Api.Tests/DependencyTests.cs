using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonfireEvents.Api.Adapters;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests
{
    public class DependencyTests
    {
        [Fact]
        public void ValidateCurrentUserId()
        {
           var authenticationService = Substitute.For<IAuthenticationService>();
           authenticationService.GetCurrentUser().Returns("bob");

           var user = authenticationService.GetCurrentUser();
           Assert.Equal("bob",user);
           

        }
    }
}
