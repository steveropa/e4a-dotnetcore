using Microsoft.AspNetCore.Server.IIS.Core;

namespace BonfireEvents.Api.Adapters
{
    public interface IAuthenticationService
    {
        string GetCurrentUser();
    }

    public class AuthenticationService : IAuthenticationService
    {
    public string GetCurrentUser()
    {
        throw new ServiceNotFoundException();
    }
  }
}