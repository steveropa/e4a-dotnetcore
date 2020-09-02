using System;

namespace BonfireEvents.Api.Domain
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