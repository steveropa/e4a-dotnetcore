namespace BonfireEvents.Api.Domain
{
  public interface ICreateEventCommand
  {
    Event Execute(string title, string description);
  }
}