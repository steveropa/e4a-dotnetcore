using System.Net;
using Shouldly;
using Xunit;

namespace BonfireEvents.Api.Tests.Api.Event
{
    public class EventApiTests : ApiTestBase
    {
        [Fact]
        public async void Retrieving_an_event_gives_200_status_code()
        {
            var response = await Client.GetAsync("/event/1");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}