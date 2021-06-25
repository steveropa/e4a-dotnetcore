using BonfireEvents.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
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

        [Test]
        public void Create_an_event()
        {
            //arrange


            //act
            var subject = new EventController();
            ActionResult<int> result = subject.Post(new CreateEventModel
            {
                Title = "My Event",
                Description = "this is an event"
            });

            //assert
            Assert.AreEqual(99, result.Value);
        }
    };
}
