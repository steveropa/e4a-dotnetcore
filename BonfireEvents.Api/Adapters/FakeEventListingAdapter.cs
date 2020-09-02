using BonfireEvents.Api.Domain;

namespace BonfireEvents.Api.Adapters
{
  public class FakeEventListingAdapter : IEventListingAdapter
  {
    public void Notify(PublishedEventMessage publishedEventMessage)
    {
      // noop
    }
  }
}