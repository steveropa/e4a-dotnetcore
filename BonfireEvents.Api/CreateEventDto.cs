using System;

namespace BonfireEvents.Api.Models
{
  public class CreateEventDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Starts { get; set; }
    public DateTime? Ends { get; set; }
    public string Status { get; set; }
    public int Capacity { get; set; }
    public TicketsModel Tickets { get; set; }
    public decimal PotentialRevenue { get; set; }
    public string Organizer { get; set; }


    public class TicketsModel
    {
      public int Capacity { get; set; }
      public decimal Cost { get; set; }
    }
  }
}