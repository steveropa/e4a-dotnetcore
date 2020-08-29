namespace BonfireEvents.Api.Domain
{
  public interface IOrganizersAdapter
  {
    Organizer GetOrganizerDetails(string userId);
  }
}