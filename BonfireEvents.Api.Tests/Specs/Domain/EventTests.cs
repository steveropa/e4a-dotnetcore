using System;
using System.Linq;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Domain.Exceptions;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests.Specs.Domain
{
  public class EventTests
  {
    [Fact]
    public void Start_here()
    {
      // An event should be created through the CreateEventCommand domain command.
      var @event = CreateEventThroughCommand();

      // The command orchestrates finding and adding organizers.
      // The CreateEventThroughCommand method provides a convenient way of mocking 
      // out interactions for testing purposes.
      Assert.Single(@event.Organizers);

      // New events are in a draft status.
      Assert.Equal(EventStates.Draft, @event.Status);

      // While getting their events ready, organizers will do a few things...

      // 1. Set a capacity for the event:
      @event.SetCapacity(20);

      // 2. Ticket the event:
      @event.AddTicketType(new TicketType(20, 0.0M, DateTime.Now.AddDays(10)));

      // 3. Schedule the event using an externally provided clock:
      Func<DateTime> clock = () => DateTime.Now;
      @event.ScheduleEvent(starts: DateTime.Now.AddDays(15), ends: DateTime.Now.AddDays(15).AddHours(3), clock);

      // 4. Lastly, publish their event
      @event.Publish(clock);
    }

    public class CreationAndRequiredData
    {
      [Fact]
      public void Event_has_a_title()
      {
        Event subject = new Event(title: @"My C# Event", description: "Monthly meet-up for enthusiasts.");

        Assert.Equal("My C# Event", subject.Title);
      }

      [Fact]
      public void Title_is_required()
      {
        Assert.Throws<CreateEventException>(() =>
          new Event(title: null, description: "Monthly meet-up for enthusiasts."));
      }

      [Fact]
      public void Event_has_a_description()
      {
        Event subject = new Event(title: @"My C# Event", description: "Monthly meet-up for enthusiasts.");

        Assert.Equal("Monthly meet-up for enthusiasts.", subject.Description);
      }

      [Fact]
      public void Description_is_required()
      {
        Assert.Throws<CreateEventException>(() => new Event(title: "My C# Event", description: null));
      }

      [Fact]
      public void Title_and_description_are_validated_together()
      {
        var ex = Assert.Throws<CreateEventException>(() => new Event(title: null, description: null));

        Assert.Contains("Title is required", ex.ValidationErrors);
        Assert.Contains("Description is required", ex.ValidationErrors);
      }

      [Fact]
      public void New_events_have_a_draft_status()
      {
        var subject = CreateDraftEvent();
        Assert.Equal(EventStates.Draft, subject.Status);
      }
    }

    public class Scheduling
    {
      [Fact]
      public void Events_have_a_start_and_end_date()
      {
        var subject = CreateDraftEvent();
        var starts = DateTime.Now.AddDays(1);
        var ends = starts.AddHours(2);

        subject.ScheduleEvent(starts: starts, ends: ends, () => DateTime.Now);

        Assert.Equal(starts, subject.Starts);
        Assert.Equal(ends, subject.Ends);
      }

      [Fact]
      public void Event_start_date_must_be_earlier_than_end_date()
      {
        var subject = CreateDraftEvent();
        var starts = DateTime.Now.AddDays(1);
        var ends = starts.AddDays(-1);

        Assert.Throws<InvalidSchedulingDatesException>(() =>
        {
          subject.ScheduleEvent(starts: starts, ends: ends, () => DateTime.Now);
        });
      }

      [Fact]
      public void Event_start_must_be_in_future()
      {
        var subject = CreateDraftEvent();

        DateTime Now() => DateTime.Now.AddDays(-2);

        subject.ScheduleEvent(DateTime.Now, DateTime.Now.AddHours(1), Now);
      }
    }

    public class Organizers
    {
      [Fact]
      public void An_event_can_have_multiple_organizers()
      {
        var subject = CreateEventThroughCommand();

        Assert.Single(subject.Organizers); // Let's verify our object factory works!

        subject.AddOrganizer(new Organizer {Id = 99, DisplayName = "Dave Laribee"});
        Assert.Equal(2, subject.Organizers.Count);
      }

      [Fact]
      public void Adding_the_same_organizer_twice_has_no_effect()
      {
        var subject = CreateEventThroughCommand();

        var alreadyAnOrganizer = subject.Organizers.First();
        subject.AddOrganizer(alreadyAnOrganizer);
        Assert.Single(subject.Organizers);
      }

      [Fact]
      public void Organizers_can_be_removed()
      {
        var subject = CreateEventThroughCommand();

        subject.AddOrganizer(new Organizer {Id = 99, DisplayName = "Dave Laribee"});
        Assert.Equal(2, subject.Organizers.Count);

        subject.RemoveOrganizer(99);
        Assert.Single(subject.Organizers);
      }

      [Fact]
      public void The_last_organizer_cannot_be_removed()
      {
        var subject = CreateEventThroughCommand();

        Assert.Throws<EventRequiresOrganizerException>(() => subject.RemoveOrganizer(subject.Organizers.First().Id));
      }

      [Fact]
      public void Removing_an_organizer_that_was_never_an_organizer_has_no_effect()
      {
        var subject = CreateEventThroughCommand();
        subject.RemoveOrganizer(3000);
      }
    }

    public class Ticketing
    {
      [Fact]
      public void Events_have_a_capacity()
      {
        var subject = CreateEventThroughCommand();
        subject.SetCapacity(50);
        Assert.Equal(50, subject.Capacity);
      }

      [Fact]
      public void Events_have_ticket_types()
      {
        var subject = CreateEventThroughCommand();
        subject.AddTicketType(new TicketType());
        Assert.Single(subject.TicketTypes);
      }

      [Fact]
      public void Tickets_have_a_max_quantity_which_cannot_exceed_the_event_capacity()
      {
        var subject = CreateEventThroughCommand();
        subject.SetCapacity(50);
        var ticketType = new TicketType(quantity: 60);

        Assert.Throws<EventCapacityExceededByTicketType>(() => subject.AddTicketType(ticketType));
      }

      [Fact]
      public void Tickets_can_have_a_cost()
      {
        var subject = new TicketType(quantity: 10, cost: 20.00M);

        Assert.Equal(20.00M, subject.Cost);
      }

      [Fact]
      public void By_default_ticket_types_are_free()
      {
        var subject = new TicketType();
        Assert.Equal(0M, subject.Cost);
      }

      [Fact]
      public void Tickets_may_not_have_a_negative_cost()
      {
        Assert.Throws<TicketsMayNotHaveNegativeCostException>(() => new TicketType(cost: -1M));
      }

      [Fact]
      public void An_event_can_have_multiple_ticket_types()
      {
        var subject = CreateEventThroughCommand();
        subject.SetCapacity(50);
        var ticketType1 = new TicketType(quantity: 20);
        var ticketType2 = new TicketType(quantity: 20);

        subject.AddTicketType(ticketType1);
        subject.AddTicketType(ticketType2);

        Assert.Equal(2, subject.TicketTypes.Count);
      }

      [Fact]
      public void Ticket_types_can_expire()
      {
        var subject = CreateEventThroughCommand();
        subject.SetCapacity(50);

        subject.ScheduleEvent(
          DateTime.Now.AddDays(10),
          DateTime.Now.AddDays(10).AddHours(1),
          () => DateTime.Now);

        var ticketType = new TicketType(quantity: 20, expires: DateTime.Now.AddDays(5));

        subject.AddTicketType(ticketType);

        Assert.Empty(subject.GetAvailableTicketTypes(DateTime.Now.AddDays(6)));
        Assert.Single(subject.GetAvailableTicketTypes(DateTime.Now.AddDays(4)));
      }
    }

    public class Publishing
    {
      [Fact]
      public void An_event_scheduled_in_the_future_can_be_published()
      {
        var subject = CreateEventThroughCommand();

        var rightNow = new Func<DateTime>(() => DateTime.Now);

        subject.ScheduleEvent(DateTime.Now.AddDays(1),
          DateTime.Now.AddDays(1).AddHours(2),
          rightNow);

        subject.SetCapacity(20);

        subject.AddTicketType(new TicketType(quantity: 20, cost: 0M, DateTime.Now.AddDays(2).AddMinutes(-10)));

        subject.Publish(rightNow);

        Assert.Equal(EventStates.Published, subject.Status);
      }

      [Fact]
      public void Events_scheduled_in_the_past_cannot_be_published()
      {
        var subject = CreateEventThroughCommand();

        subject.ScheduleEvent(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2).AddHours(2),
          () => DateTime.Now.AddDays(-3));

        Assert.Throws<EventsScheduledInPastCannotBePublishedException>(() => subject.Publish(() => DateTime.Now));
      }

      [Fact]
      public void Events_that_are_not_scheduled_cannot_be_published()
      {
        var subject = CreateEventThroughCommand();

        Assert.Throws<UnscheduledEventsCannotBePublishedException>(() => subject.Publish(() => DateTime.Now));
      }

      [Fact]
      public void An_event_requires_tickets_to_be_published()
      {
        var subject = CreateEventThroughCommand();

        subject.ScheduleEvent(
          DateTime.Now.AddDays(2),
          DateTime.Now.AddDays(2).AddHours(2),
          () => DateTime.Now);

        subject.SetCapacity(20);


        Assert.Throws<AnEventRequiresTicketsToBePublishedException>(() => subject.Publish(() => DateTime.Now));
      }
    }

    /// <summary>
    /// Creates an event using the CreateEvent factory. The returned event
    /// will have an `Organizer` added.
    /// </summary>
    /// <returns>An Event Entity</returns>
    private static Event CreateEventThroughCommand()
    {
      var mockAuthAdapter = Substitute.For<IAuthenticationAdapter>();
      var mockOrganizerAdapter = Substitute.For<IOrganizersAdapter>();

      mockAuthAdapter.GetCurrentUser().Returns("bobross");
      mockOrganizerAdapter.GetOrganizerDetails(Arg.Any<string>())
        .Returns(new Organizer {Id = 10, DisplayName = "Bob Ross"});

      var service = new CreateEventCommand(mockAuthAdapter, mockOrganizerAdapter);

      var theEvent = service.Execute("My Event", "A gathering of like-minded folk.");

      return theEvent;
    }

    /// <summary>
    /// Factory / Object Mother that eases creation of a draft event.
    /// </summary>
    /// <returns>An event in a draft state.</returns>
    private static Event CreateDraftEvent()
    {
      return new Event("My Event", "Periodic gathering of like-minded folk.");
    }
  }
}