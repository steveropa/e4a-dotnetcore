using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonfireEvents.Api.Domain
{
    public class Event
    {
        public string Name { get; }
        public string Description { get; }

        public Event(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
