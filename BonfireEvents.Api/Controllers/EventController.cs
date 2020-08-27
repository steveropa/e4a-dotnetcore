using System;
using BonfireEvents.Api.Domain;
using BonfireEvents.Api.Models;
using BonfireEvents.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<EventDetailModel> Get(int id)
    {
      Event theEvent = _repository.Find(id);

      var mappedModel = new EventDetailModel()
      {
        Title = theEvent.Title,
        Description = theEvent.Description
      };

      return mappedModel;
    }

    [HttpPost]
    public IActionResult Post(CreateEventModel createEventModel)
    {
      var newEvent = new Event(title: createEventModel.Title, description: createEventModel.Description);
      var id = _repository.Save(newEvent);
      return Created($"/event/{id.ToString()}", null);
    }
  }
}