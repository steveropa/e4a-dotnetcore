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
            catch (Exception error)
            {
                Assert.IsType<ArgumentNullException>(error);
            }

        }

        [Fact]
        public void MessageIfTitleIsMissing()
        {
            var subject = new EventController();
            CreateEventDto eventDto = new CreateEventDto();
            eventDto.Description = "Ok, its really CGI but still...";
            try
            {
                subject.Post(eventDto);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentException>(e);
            }
        }

        [Fact]
        public void MessageIfDescriptionIsMissing()
        {
            var subject = new EventController();
            CreateEventDto eventDto = new CreateEventDto();
            eventDto.Title = "Fiserv on Parade";
            try
            {
                subject.Post(eventDto);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentException>(e);

            }



        }

        [Fact]
        public void ArgumentexceptionIfDatesAreWrong()
        {
            var subject = new EventController();
            CreateEventDto eventDto = new CreateEventDto();
            eventDto.Title = "Curt";
            eventDto.Description = "Curt is so awesome";
        }

}
}