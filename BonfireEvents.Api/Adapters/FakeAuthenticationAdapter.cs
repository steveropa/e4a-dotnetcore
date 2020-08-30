using BonfireEvents.Api.Domain;

namespace BonfireEvents.Api.Adapters
{
  public class FakeAuthenticationAdapter : IAuthenticationAdapter
  {
    public string GetCurrentUser()
    {
      return "dave";
    }
  }
}