namespace BonfireEvents.Api.Domain
{
  public interface IEventListingAdapter
  {
    void Notify(PublishedEventMessage publishedEventMessage);
  }
}