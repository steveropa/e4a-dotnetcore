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
      private IAuthenticationService AuthService;

      EventController(IAuthenticationService authenticationService, IOrganizersService organizerService)
      {
         AuthService = authenticationService;

      }

        public EventController()
        {
        }

        [HttpPost]
    public IActionResult Post(CreateEventDto eventData)
    {
        ValidateEventData(eventData);

        var organizer = new OrganizersService().GetOrganizerDetails( new AuthenticationService().GetCurrentUser());

      eventData.Organizer = organizer.DisplayName;

      

      if (eventData.Status == "Published")
      {
          NotifyOrganizer(eventData);
      }

      new DataAccessLayer().Save(eventData);
      
      return Ok();
    }

    private static void ValidateEventData(CreateEventDto eventData)
    {
        if (eventData == null)
        {
            throw new ArgumentNullException("eventData is null");
        }

        if (eventData.Description == null) throw new ArgumentException();
        if (eventData.Title == null) throw new ArgumentException();
        if (eventData.Starts > eventData.Ends)
        {
            throw new ArgumentException();
        }
        }

    private static void NotifyOrganizer(CreateEventDto eventData)
    {
        if (eventData.Starts < DateTime.Now)
        {
            throw new ArgumentException();
        }

        if (eventData.Capacity <= 0) throw new ArgumentException();
        if (eventData.Tickets.Capacity != eventData.Capacity) throw new ArgumentException();

        decimal potentialRevenue = eventData.Tickets.Capacity * eventData.Tickets.Cost;
        eventData.PotentialRevenue = potentialRevenue;

        new EventListingManager().Notify(eventData);
    }

    private decimal CalculatePotentialRevenue(CreateEventDto createEventDto)
    {
      return (createEventDto.Tickets.Capacity * createEventDto.Tickets.Cost);
    }
  }
}