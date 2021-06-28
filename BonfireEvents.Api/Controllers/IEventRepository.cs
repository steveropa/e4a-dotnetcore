using BonfireEvents.Api.Domain;
using System;

namespace BonfireEvents.Api.Controllers
{
    public interface IEventRepository
    {
        Event Find(int id);
        int Save(Event anEvent);
    }
}