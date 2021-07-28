using System;
using BonfireEvents.Api.Controllers;
using BonfireEvents.Api.Models;
using NSubstitute.ExceptionExtensions;
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

    [Fact]
    public void ShowMessageIfThereIsNoEventData()
    {
        //Arrange(Given)
        var subject = new EventController();
        CreateEventDto eventDto = null;

        //Act(When)
        try
        {
            subject.Post(eventDto);
        }
        catch (ArgumentNullException error)
        {
            Assert.IsType<ArgumentNullException>(error);
        }
       
    }
  }
}