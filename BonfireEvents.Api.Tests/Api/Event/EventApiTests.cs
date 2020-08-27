using System.Net;
using BonfireEvents.Api.Tests.Api.Framework;
using BonfireEvents.Api.ViewModels;
using Newtonsoft.Json;
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

        [Fact]
        public async void Retrieving_an_event_provides_an_event_detail_model_inside_the_request_body()
        {
            var response = await Client.GetAsync("/event/1");
            var rawBody = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<EventDetailModel>(rawBody);
            
            model.ShouldBeOfType<EventDetailModel>();
        }
    }
}