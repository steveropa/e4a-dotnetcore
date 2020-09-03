using System;
using BonfireEvents.Domain.Event.Adapters;

namespace BonfireEvents.Domain.Event.Commands
{
  public class PublishEventCommand : IPublishEventCommand
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