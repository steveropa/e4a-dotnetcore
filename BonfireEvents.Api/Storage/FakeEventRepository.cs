using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonfireEvents.Domain.Event;
using BonfireEvents.Domain.Event.Adapters;

namespace BonfireEvents.Api.Storage
{
  /// <summary>
  /// Since we haven't decided on a persistence technology yet, we'll
  /// rely on a fake for exploratory testing purposes (testing through
  /// browser, PostMan, etc. w/ API running).
  /// </summary>
  public class FakeEventRepository : IEventRepository
  {
    private Dictionary<int, Event> inMemoryEvents = new Dictionary<int, Event>();
    
    public FakeEventRepository()
    {
      AddSampleData();
    }

    public Event Find(int id)
    {
      return inMemoryEvents[id];
    }

    public int Save(Event anEvent)
    {
      var newId = GetNextId();
      anEvent.Id = newId;
      
      inMemoryEvents.Add(newId, anEvent);
      return newId;
    }

    private int GetNextId()
    {
      return inMemoryEvents.Keys.Max() + 1;
    }

    private void AddSampleData()
    {
      inMemoryEvents.Add(1, new Event(title:"Fake", "Fake"));
      inMemoryEvents.Add(2, new Event(title:"Meetup", "My Meetup"));
      inMemoryEvents.Add(3, new Event(title:"Meetup", "My Meetup"));
      inMemoryEvents.Add(4, new Event(title:"Meetup", "My Meetup"));
      inMemoryEvents.Add(5, new Event(title:"Meetup", "My Meetup"));
      inMemoryEvents.Add(6, new Event(title:"Meetup", "My Meetup"));
    }
  }
}