using System;

namespace BonfireEvents.Api.Adapters
{
  public class OrganizersService 
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