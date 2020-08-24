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

    public string Title { get; }
  }

  public class Event
  {
    public Event(string title)
    {
      Title = title;
    }

    public string Title { get; }
  }
}