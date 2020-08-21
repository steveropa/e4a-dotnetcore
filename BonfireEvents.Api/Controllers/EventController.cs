using Microsoft.AspNetCore.Mvc;

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
        
    }
}
