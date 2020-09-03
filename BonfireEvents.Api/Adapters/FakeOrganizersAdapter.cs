using BonfireEvents.Domain.Event;
using BonfireEvents.Domain.Event.Adapters;

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