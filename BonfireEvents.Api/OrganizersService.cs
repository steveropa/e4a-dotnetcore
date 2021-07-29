using System;

namespace BonfireEvents.Api.Adapters
{
    public interface IOrganizersService
    {
        Organizer GetOrganizerDetails(string userId);
    }

    public class OrganizersService : IOrganizersService
    {
    public Organizer GetOrganizerDetails(string userId)
    {
      throw new ServiceNotFoundException();
    }
  }

  public class ServiceNotFoundException : Exception
  {
  }

  public class Organizer
  {
    public string DisplayName { get; set; }
  }
}