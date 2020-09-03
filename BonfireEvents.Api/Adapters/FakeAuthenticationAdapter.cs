using BonfireEvents.Domain.Event;
using BonfireEvents.Domain.Event.Adapters;

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