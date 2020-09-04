using BonfireEvents.Api.Controllers;
using BonfireEvents.Api.Models;
using Xunit;

namespace BonfireEvents.Api.Tests
{
  public class SafetyNet
  {
    [Fact]
    public void Write_a_test()
    {
      var subject = new EventController();
      subject.Post(new CreateEventDto());
    }
  }
}