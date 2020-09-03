namespace BonfireEvents.Domain.Event.Adapters
{
  public interface IOrganizersAdapter
  {
    Organizer GetOrganizerDetails(string userId);
  }
}