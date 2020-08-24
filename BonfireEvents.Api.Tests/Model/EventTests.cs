using System;
using System.Collections.Generic;
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
  }

  public class Event
  {
    public Event(string title, string description)
    {
      if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description)) throw new CreateEventException();
      Title = title;
      Description = description;
    }

    public string Title { get; }
    public string Description { get; }
  }

  public class CreateEventException : Exception
  {
  }
}