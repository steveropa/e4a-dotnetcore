using System;

namespace BonfireEvents.Api.Domain
{
  public interface IEventRepository
  {
    Event Find(int id);
    int Save(Event anEvent);
  }
}