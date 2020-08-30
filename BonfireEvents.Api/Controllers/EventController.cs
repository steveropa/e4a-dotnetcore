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
    private readonly ICreateEventCommand _createEventCommand;

    public EventController(IEventRepository repository, ICreateEventCommand createEventCommand)
    {
      _repository = repository;
      _createEventCommand = createEventCommand;
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
      var anotherEvent =
        _createEventCommand.Execute(title: createEventModel.Title, description: createEventModel.Description);
      var id = _repository.Save(anotherEvent);
      return Created($"/event/{id.ToString()}", null);
    }
  }
}