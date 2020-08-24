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
      Event subject = new Event(title: @"My C# Event");
      
      Assert.Equal("My C# Event", subject.Title);
    }
    
    [Fact]
    public void Title_is_required()
    {
      Assert.Throws<CreateEventException>(() => new Event(title: null));
    }
  }

  public class Event
  {
    public Event(string title)
    {
      if (string.IsNullOrEmpty(title)) throw new CreateEventException();
      Title = title;
    }

    public string Title { get; }
  }

  public class CreateEventException : Exception
  {
  }
}