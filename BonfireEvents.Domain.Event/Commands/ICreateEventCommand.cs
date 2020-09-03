namespace BonfireEvents.Domain.Event.Commands
{
  public interface ICreateEventCommand
  {
    Event Execute(string title, string description);
  }
}