namespace BonfireEvents.Api.Model
{
  public interface IEventRepository
  {
    Event Find(int id);
  }
}