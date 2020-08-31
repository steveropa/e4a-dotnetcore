using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BonfireEvents.Api.Exceptions;

namespace BonfireEvents.Api.Domain
{
  public class Event
  {
    private readonly List<Organizer> _organizers = new List<Organizer>();

    public Event(string title, string description)
    {
      ValidateEventData(title, description);

      Title = title;
      Description = description;
    }

    public int Id { get; internal set; }

    public string Title { get; }

    public string Description { get; }

    public DateTime Starts { get; private set; }

    public DateTime Ends { get; private set; }

    public string Status => EventStates.Draft;

    public ImmutableList<Organizer> Organizers => _organizers.ToImmutableList();
    
    public int Capacity { get; private set; }

    public void ScheduleEvent(DateTime starts, DateTime ends, Func<DateTime> now)
    {
      if (starts < now()) throw new InvalidSchedulingDatesException();
      if (starts > ends) throw new InvalidSchedulingDatesException();

      Starts = starts;
      Ends = ends;
    }

    public void AddOrganizer(Organizer organizer)
    {
      if (_organizers.Any(o => o.Id == organizer.Id)) return;
      _organizers.Add(organizer);
    }

    public void RemoveOrganizer(int organizerId)
    {
      var organizer = _organizers.Find(o => o.Id == organizerId);
      if ((organizer != null && organizer.Id == organizerId) && (_organizers.Count == 1))
        throw new EventRequiresOrganizerException();
      _organizers.Remove(organizer);
    }

    public void SetCapacity(int maximum)
    {
      Capacity = maximum;
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