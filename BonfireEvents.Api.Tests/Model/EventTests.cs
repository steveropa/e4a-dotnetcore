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
  }

  public class Event
  {
    public Event(string title, string description)
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
      
      Title = title;
      Description = description;
    }

    public string Title { get; }
    public string Description { get; }
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