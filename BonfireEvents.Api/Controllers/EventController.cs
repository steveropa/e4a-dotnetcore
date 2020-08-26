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
    public ActionResult<EventViewModel> Get(int id)
    {
      Event theEvent = _repository.Find(id);

      var mappedModel = new EventViewModel()
      {
        Title = theEvent.Title,
        Description = theEvent.Description
      };

      return new ActionResult<EventViewModel>(mappedModel);
    }

    [HttpPost]
    public ActionResult<int> Post(CreateEventModel createEventModel)
    {
      Event newEvent = new Event(title: createEventModel.Title, description: createEventModel.Description);

      return new ActionResult<int>(_repository.Save(newEvent));
    }
  }
}