using System;
using BonfireEvents.Domain.Event.Exceptions;

namespace BonfireEvents.Domain.Event
{
  public class TicketType
  {
    public TicketType(int quantity = 0, decimal cost = 0M, DateTime expires = default)
    {
      if (cost < 0) throw new TicketsMayNotHaveNegativeCostException();
      Quantity = quantity;
      Cost = cost;
      Expires = expires;
    }

    public int Quantity { get; }
    public decimal Cost { get; }
    public DateTime Expires { get; }
  }
}