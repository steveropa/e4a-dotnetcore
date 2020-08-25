using System;
using BonfireEvents.Api.Exceptions;
using BonfireEvents.Api.Model;
using Xunit;

namespace BonfireEvents.Api.Tests.Model
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
      var subject = new Event("Meetup", description: "Periodic meeting");
      var starts = DateTime.Now.AddDays(1);
      var ends = starts.AddHours(2);
      
      subject.ScheduleEvent(starts: starts, ends: ends, ()=> DateTime.Now);
      
      Assert.Equal(starts, subject.Starts);
      Assert.Equal(ends, subject.Ends);
    }

    [Fact]
    public void Event_start_date_must_be_earlier_than_end_date()
    {
      var subject = new Event("Meetup", description: "Periodic meeting");
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
      var subject = new Event("Title", "Desc");

      DateTime Now() => DateTime.Now.AddDays(-2);

      subject.ScheduleEvent(DateTime.Now, DateTime.Now.AddHours(1), Now);
    }
  }
}