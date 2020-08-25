using System;
using System.Collections.Generic;
using System.Linq;
using BonfireEvents.Api.Exceptions;

namespace BonfireEvents.Api.Model
{
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
}