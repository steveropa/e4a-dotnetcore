namespace BonfireEvents.Domain.Event.Adapters
{
  public interface IAuthenticationAdapter
  {
    string GetCurrentUser();
  }
}