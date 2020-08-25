using System;
using System.Collections.Generic;
using System.Linq;
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

  public class Event
  {
    public Event(string title, string description)
    {
      ValidateEventData(title, description);

      Title = title;
      Description = description;
    }

    public string Title { get; }
    public string Description { get; }
    public DateTime Starts { get; private set; }
    public DateTime Ends { get; private set; }

    public void ScheduleEvent(DateTime starts, DateTime ends, Func<DateTime> now)
    {
      if (starts < now()) throw new InvalidSchedulingDatesException();
      if (starts > ends) throw new InvalidSchedulingDatesException();
      
      Starts = starts;
      Ends = ends;
    }

    private static void ValidateEventData(string title, string description)
    {
      var errors = new List<string>();

      if (string.IsNullOrEmpty(title)) errors.Add("Title is required");
      if (string.IsNullOrEmpty(description)) errors.Add("Description is required");

      if (errors.Any())
      {
        var ex = new CreateEventException();
        ex.ValidationErrors.AddRange(errors);
        throw ex;
      }
    }
  }

  public class InvalidSchedulingDatesException : Exception
  {
  }

  public class CreateEventException : Exception
  {
    public CreateEventException()
    {
      this.ValidationErrors = new List<string>();
    }
    
    public List<string> ValidationErrors { get; }
  }
}