using BonfireEvents.Api.Domain.Exceptions;

namespace BonfireEvents.Api.Domain
{
  public class TicketType
  {
    public TicketType(int quantity = 0, decimal cost = 0M)
    {
      if (cost < 0) throw new TicketsMayNotHaveNegativeCostException();
      Quantity = quantity;
      Cost = cost;
    }

    public int Quantity { get; }
    public decimal Cost { get; }
  }
}