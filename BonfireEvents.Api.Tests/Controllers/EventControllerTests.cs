using BonfireEvents.Api.Controllers;
using NUnit;
using NUnit.Framework;

namespace BonfireEvents.Api.Tests.Controllers
{
    public class EventControllerTests
    {
        [Test]
        public void Get_returns_hello_world()
        {
            var subject = new EventController();
            var result = subject.Get();
            Assert.AreEqual("Hello World", actual: result);
        }
    }
}
