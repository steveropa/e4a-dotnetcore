using BonfireEvents.Api.Domain;

namespace BonfireEvents.Api.Adapters
{
  public class FakeOrganizersAdapter : IOrganizersAdapter
  {
    public Organizer GetOrganizerDetails(string userId)
    {
      return new Organizer {Id = 99, DisplayName = "Dave Laribee"};
    }
  }
}