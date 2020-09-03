using BonfireEvents.Domain.Event.Commands;

namespace BonfireEvents.Domain.Event.Adapters
{
  public interface IEventListingAdapter
  {
    void Notify(PublishedEventMessage publishedEventMessage);
  }
}