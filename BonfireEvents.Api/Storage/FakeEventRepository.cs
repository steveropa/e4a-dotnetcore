using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonfireEvents.Api.Controllers;
using BonfireEvents.Api.Domain;

namespace BonfireEvents.Api.Storage
{
    public class FakeEventRepository : IEventRepository
    {
        public Event Find(int id)
        {
            return new Event("fake", "fake");
        }

        public int Save(Event anEvent)
        {
            throw new NotImplementedException();
        }
    }
}
