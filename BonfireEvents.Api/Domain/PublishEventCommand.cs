using System;

namespace BonfireEvents.Api.Domain
{
  public class PublishEventCommand
  {
    private readonly IEventListingAdapter _eventListingAdapter;

    public PublishEventCommand(IEventListingAdapter eventListingAdapter)
    {
      _eventListingAdapter = eventListingAdapter;
    }

    public void Execute(Event @event)
    {
      @event.Publish(() => DateTime.Now);

      var publishedEventMessage = new PublishedEventMessage
      {
        Id = @event.Id, Title = @event.Title, Description = @event.Description, Starts = @event.Starts.Value,
        Ends = @event.Ends.Value
      };

      _eventListingAdapter.Notify(publishedEventMessage);
    }
  }
}