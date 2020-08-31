using System;
using System.Linq;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Exceptions;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests.Domain
{
  public class EventTests
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
      Assert.Throws<CreateEventException>(() => new Event(title: null, description: "Monthly meet-up for enthusiasts."));
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
    public void Events_have_a_start_and_end_date()
    {
      var subject = CreateDraftEvent();
      var starts = DateTime.Now.AddDays(1);
      var ends = starts.AddHours(2);
      
      subject.ScheduleEvent(starts: starts, ends: ends, ()=> DateTime.Now);
      
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
        subject.ScheduleEvent(starts: starts, ends: ends, ()=> DateTime.Now);
      });
    }

    [Fact]
    public void Event_start_must_be_in_future()
    {
      var subject = CreateDraftEvent();

      DateTime Now() => DateTime.Now.AddDays(-2);

      subject.ScheduleEvent(DateTime.Now, DateTime.Now.AddHours(1), Now);
    }

    [Fact]
    public void New_events_have_a_draft_status()
    {
      var subject = CreateDraftEvent();
      Assert.Equal(EventStates.Draft, subject.Status);
    }

    [Fact]
    public void An_event_can_have_multiple_organizers()
    {
      var subject = CreateEventThroughCommand();

      Assert.Single(subject.Organizers); // Let's verify our object factory works!
      
      subject.AddOrganizer(new Organizer{Id=99, DisplayName = "Dave Laribee"});
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
      
      subject.AddOrganizer(new Organizer{Id=99, DisplayName = "Dave Laribee"});
      Assert.Equal(2, subject.Organizers.Count);

      subject.RemoveOrganizer(99);
      Assert.Equal(1, subject.Organizers.Count);
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
        var ticketType = new TicketType { Quantity = 51};

        Assert.Throws<EventCapacityExceededByTicketType>(() => subject.AddTicketType(ticketType));
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
      mockOrganizerAdapter.GetOrganizerDetails(Arg.Any<string>()).Returns(new Organizer{Id = 10, DisplayName = "Bob Ross"});
      
      var service = new CreateEventCommand(mockAuthAdapter, mockOrganizerAdapter);

      var theEvent = service.Execute("My Event", "A gathering of like-minded folk.");

      return theEvent;
    }

    /// <summary>
    /// Factory / Object Mother that eases creation of a draft event.
    /// </summary>
    /// <returns>An event in a draft state.</returns>
    private Event CreateDraftEvent()
    {
      return new Event("My Event", "Periodic gathering of like-minded folk.");
    }
  }
}