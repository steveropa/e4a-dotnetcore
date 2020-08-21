using BonfireEvents.Api.Controllers;
using Xunit;

namespace BonfireEvents.Api.Tests.Controllers
{
    public class EventControllerTests
    {
        [Fact]
        public void Get_returns_hello_world()
        {
            var subject = new EventController();
            var result = subject.Get();
            Assert.Equal("Hello World", actual: result);
        }
    }
}
