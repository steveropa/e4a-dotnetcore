using System;
using BonfireEvents.Api.Adapters;
using BonfireEvents.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BonfireEvents.Api.Controllers
{
  [ApiController]
  [Route("event")]
  public class EventController : ControllerBase
  {

    [HttpPost]
    public IActionResult Post(CreateEventDto m)
    {
      if (m.Description == null) throw new ArgumentException();
      if (m.Title == null) throw new ArgumentException();
      
      var organizer = new OrganizersService().GetOrganizerDetails( new AuthenticationService().GetCurrentUser());

      m.Organizer = organizer.DisplayName;

      if (m.Starts > m.Ends)
      {
        throw new ArgumentException();
      }

      if (m.Status == "Published")
      {
        if (m.Starts < DateTime.Now) 
        {
          throw new ArgumentException();
        }
        if (m.Capacity <= 0) throw new ArgumentException(); 
        if (m.Tickets.Capacity != m.Capacity) throw new ArgumentException();

        decimal pr = m.Tickets.Capacity * m.Tickets.Cost;
        m.PotentialRevenue = pr;
        
        new EventListingManager().Notify(m);
      }

      new DataAccessLayer().Save(m);
      
      return Ok();
    }

    private decimal CalculatePotentialRevenue(CreateEventDto createEventDto)
    {
      return (createEventDto.Tickets.Capacity * createEventDto.Tickets.Cost);
    }
  }
}