using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using BonfireEvents.Api.Models;
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

    [Fact]
    public async void Creating_new_events_gives_a_201_status_code()
    {
      var payload = new CreateEventModel
      {
        Title = "My Event",
        Description = "A description."
      };
      
      var payloadAsString = JsonConvert.SerializeObject(payload);

      HttpContent content = new StringContent(payloadAsString);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      var response = await Client.PostAsync("/event/", content);
      response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
  }
}