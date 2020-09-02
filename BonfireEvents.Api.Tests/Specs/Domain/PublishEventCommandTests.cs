using System;
using System.Collections.Generic;
using BonfireEvents.Api.Domain;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace BonfireEvents.Api.Tests.Specs.Domain
{
  public class PublishEventCommandTests
  {
    [Fact]
    public void Publishing_an_event_notifies_the_event_listing_service()
    {
      var eventListingAdapter = Substitute.For<IEventListingAdapter>();

      var subject = new PublishEventCommand(eventListingAdapter);

      var @event = CreatePublishableEvent();

      subject.Execute(@event);

      eventListingAdapter.Received().Notify(Arg.Any<PublishedEventMessage>());
      Assert.Equal(EventStates.Published, @event.Status);
    }

    [Fact]
    public void Maps_the_event_to_a_published_event_message()
    {
      var eventListingAdapter = Substitute.For<IEventListingAdapter>();

      PublishedEventMessage message = null;
      eventListingAdapter.Notify(Arg.Do<PublishedEventMessage>(eventMessage => message = eventMessage));

      var subject = new PublishEventCommand(eventListingAdapter);
      var @event = CreatePublishableEvent();

      subject.Execute(@event);

      Assert.NotNull(message);

      Assert.Equal(@event.Title, message.Title);
      Assert.Equal(@event.Description, message.Description);
      Assert.Equal(@event.Starts, message.Starts);
      Assert.Equal(@event.Ends, message.Ends);
    }

    private static Event CreatePublishableEvent()
    {
      var @event = EventTests.CreateEventThroughCommand();

      @event.SetCapacity(10);
      @event.ScheduleEvent(starts: DateTime.Now.AddDays(6), ends: DateTime.Now.AddDays(6).AddHours(1),
        () => DateTime.Now);
      @event.AddTicketType(new TicketType(quantity: 10, cost: 0M, expires: DateTime.Now.AddDays(5)));
      return @event;
    }
  }
}