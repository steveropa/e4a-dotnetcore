using BonfireEvents.Api.Model;
using BonfireEvents.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BonfireEvents.Api.Controllers
{
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventController(IEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("event/{id}")]
        public EventViewModel Get(int id)
        {
            Event theEvent = _repository.Find(id);
            
            return new EventViewModel()
            {
                Title = theEvent.Title,
                Description = theEvent.Description
            };
        }
        
    }
}
