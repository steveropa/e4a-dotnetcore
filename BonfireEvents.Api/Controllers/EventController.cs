using Microsoft.AspNetCore.Mvc;
using System;

namespace BonfireEvents.Api.Controllers
{
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        public ActionResult<int> Post(CreateEventModel createEventModel)
        {
            return new ActionResult<int>(99);
        }
    }
}
