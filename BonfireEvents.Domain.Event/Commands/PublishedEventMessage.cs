using System;

namespace BonfireEvents.Domain.Event.Commands
{
  public class PublishedEventMessage
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Starts { get; set; }
    public DateTime Ends { get; set; }
  }
}