using System.Linq;
using BonfireEvents.Api.Domain;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests.Domain
{
  public class CreateEventTests
  {
    [Fact]
    public void Retrieve_the_organizer_data_for_the_currently_login()
    {
      // Arrange
      var mockAuthenticationAdapter = Substitute.For<IAuthenticationAdapter>();
      var mockOrganizersAdapter = Substitute.For<IOrganizersAdapter>();

      mockAuthenticationAdapter.GetCurrentUser().Returns("dave");

      var subject = new CreateEvent(mockAuthenticationAdapter, mockOrganizersAdapter);

      // Act
      subject.Execute("My Event", "My Description");

      // Assert
      mockOrganizersAdapter.Received().GetOrganizerDetails("dave");
    }

    [Fact]
    public void New_events_get_the_currently_logged_in_organizer()
    {
      // Arrange
      var mockAuthenticationAdapter = Substitute.For<IAuthenticationAdapter>();
      var mockOrganizersAdapter = Substitute.For<IOrganizersAdapter>();

      mockAuthenticationAdapter.GetCurrentUser().Returns("dave");
      
      var theOrganizer = new Organizer {Id = 99, DisplayName = "David Laribee"};
      mockOrganizersAdapter.GetOrganizerDetails("dave").Returns(theOrganizer);

      var subject = new CreateEvent(mockAuthenticationAdapter, mockOrganizersAdapter);

      // Act
      var theEvent = subject.Execute("My Event", "My Description");

      // Assert
      Assert.Single(theEvent.Organizers);
      Assert.Equal(theEvent.Organizers.First(), theOrganizer);
    }
  }

  public class CreateEvent
  {
    private readonly IAuthenticationAdapter _authenticationAdapter;
    private readonly IOrganizersAdapter _organizersAdapter;

    public CreateEvent(IAuthenticationAdapter authenticationAdapter, IOrganizersAdapter organizersAdapter)
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

  public interface IAuthenticationAdapter
  {
    string GetCurrentUser();
  }

  public interface IOrganizersAdapter
  {
    Organizer GetOrganizerDetails(string userId);
  }
}