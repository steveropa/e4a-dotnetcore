namespace BonfireEvents.Api.Domain
{
  public class CreateEventCommand : ICreateEventCommand
  {
    private readonly IAuthenticationAdapter _authenticationAdapter;
    private readonly IOrganizersAdapter _organizersAdapter;

    public CreateEventCommand(IAuthenticationAdapter authenticationAdapter, IOrganizersAdapter organizersAdapter)
    {
      _authenticationAdapter = authenticationAdapter;
      _organizersAdapter = organizersAdapter;
    }


    public Event Execute(string title, string description)
    {
      var userId = _authenticationAdapter.GetCurrentUser();
      var organizer = _organizersAdapter.GetOrganizerDetails(userId);

      var theEvent = new Event(title, description);
      theEvent.AddOrganizer(organizer);

      return theEvent;
    }
  }
}