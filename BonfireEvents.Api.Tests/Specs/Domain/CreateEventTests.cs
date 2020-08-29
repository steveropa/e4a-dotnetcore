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
      _organizersAdapter.GetOrganizerDetails(userId);
      return null;
    }
  }

  public interface IAuthenticationAdapter
  {
    string GetCurrentUser();
  }

  public interface IOrganizersAdapter
  {
    void GetOrganizerDetails(string userId);
  }
}