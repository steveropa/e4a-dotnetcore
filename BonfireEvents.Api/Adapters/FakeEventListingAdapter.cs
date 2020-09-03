using BonfireEvents.Domain.Event;
using BonfireEvents.Domain.Event.Adapters;
using BonfireEvents.Domain.Event.Commands;

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