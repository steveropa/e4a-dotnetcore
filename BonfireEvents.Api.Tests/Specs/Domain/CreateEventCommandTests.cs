using System.Linq;
using BonfireEvents.Api.Domain;
using NSubstitute;
using Xunit;

namespace BonfireEvents.Api.Tests.Domain
{
  public class CreateEventCommandTests
  {
    [Fact]
    public void Retrieve_the_organizer_data_for_the_currently_login()
    {
      // Arrange
      var mockAuthenticationAdapter = Substitute.For<IAuthenticationAdapter>();
      var mockOrganizersAdapter = Substitute.For<IOrganizersAdapter>();

      mockAuthenticationAdapter.GetCurrentUser().Returns("dave");

      var subject = new CreateEventCommand(mockAuthenticationAdapter, mockOrganizersAdapter);

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

      var subject = new CreateEventCommand(mockAuthenticationAdapter, mockOrganizersAdapter);

      // Act
      var theEvent = subject.Execute("My Event", "My Description");

      // Assert
      Assert.Single(theEvent.Organizers);
      Assert.Equal(theEvent.Organizers.First(), theOrganizer);
    }
  }
}