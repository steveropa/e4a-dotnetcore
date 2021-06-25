using Microsoft.AspNetCore.Mvc;
using System;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Models;

namespace BonfireEvents.Api.Controllers
{
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventController(IEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(template: "{id}")]
        public ActionResult<EventViewModel> Get(int id)
        {
           Event theEvent = _repository.Find(id);
           var mappedModel = new EventViewModel()
           {
               Title = theEvent.Name,
               Description = theEvent.Description
           };

           return new ActionResult<EventViewModel>(mappedModel);
        }

        public ActionResult<int> Post(CreateEventModel createEventModel)
        {
            
            return new ActionResult<int>(99);
        }
    }
}
