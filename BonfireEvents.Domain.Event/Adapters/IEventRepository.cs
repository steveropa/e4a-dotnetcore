namespace BonfireEvents.Domain.Event.Adapters
{
  public interface IEventRepository
  {
    Event Find(int id);
    int Save(Event anEvent);
  }
}