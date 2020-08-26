using BonfireEvents.Api.Controllers;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Models;
using BonfireEvents.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests.Controllers
{
  public class EventControllerTests
  {
    [Fact]
    public void Retrieve_the_event_from_a_repository()
    {
      var repository = Substitute.For<IEventRepository>();
      repository.Find(1).Returns(new Event("My Event", "A description"));

      new EventController(repository).Get(1);

      repository.Received().Find(1);
    }

    [Fact]
    public void Returns_the_requested_event()
    {
      var repository = Substitute.For<IEventRepository>();
      var theEvent = new Event("My Event", "A description");
      repository.Find(1).Returns(theEvent);

      var subject = new EventController(repository);

      EventViewModel viewModel = subject.Get(1).Value;

      Assert.Equal(theEvent.Title, viewModel.Title);
      Assert.Equal(theEvent.Description, viewModel.Description);
    }

    [Fact]
    public void Creating_an_event()
    {
      var repository = Substitute.For<IEventRepository>();
      repository.Save(Arg.Any<Event>()).Returns(99);
      var subject = new EventController(repository);

      ActionResult<int> eventId = subject.Post(new CreateEventModel
        {Title = "My Event", Description = "A description."});

      Assert.Equal(99, eventId.Value);
    }
  }
}