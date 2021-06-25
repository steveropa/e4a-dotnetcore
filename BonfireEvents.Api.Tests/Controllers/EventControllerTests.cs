using BonfireEvents.Api.Controllers;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace BonfireEvents.Api.Tests.Controllers
{
    public class EventControllerTests
    {
        private IEventRepository _eventRepository;

        [SetUp]
        public void Setup()
        {
            _eventRepository = Substitute.For<IEventRepository>();
        }

        [Test]
        public void Get_an_event_from_a_repository()
        {
            _eventRepository.Find(1).Returns(new Event("My Event","A Description"));

            new EventController(_eventRepository).Get(1);
            _eventRepository.Received().Find(1);
        }

        [Test]
        public void Create_an_event()
        {
            _eventRepository.Save((Arg.Any<Event>())).Returns(99);
       
            var subject = new EventController(_eventRepository);
            ActionResult<int> result = subject.Post(new CreateEventModel
            {
                Title = "My Event",
                Description = "this is an event"
            });
            Assert.AreEqual(99, result.Value);
        }

        [Test]
        public void Returns_the_requested_event()
        {
            Event theEvent = new Event("My Event", "A description");
            _eventRepository.Find(1).Returns(theEvent);

            var subject = new EventController(_eventRepository);
            EventViewModel viewModel = subject.Get(1).Value;

            Assert.AreEqual(theEvent.Name,viewModel.Title);
        }
        
    };
}
