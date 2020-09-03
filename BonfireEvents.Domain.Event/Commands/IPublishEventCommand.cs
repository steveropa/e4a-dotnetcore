namespace BonfireEvents.Domain.Event.Commands
{
  public interface IPublishEventCommand
  {
    void Execute(Event @event);
  }
}