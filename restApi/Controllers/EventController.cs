using Microsoft.AspNetCore.Mvc;
using restApi.Dtos.Event;
using restApi.Services;

namespace restApi.Controllers;

[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    [HttpPost("create")]
    public IActionResult CreateEvent([FromHeader] string jwt, [FromBody] EventCreateDto eventDto)
    {
        var newEvent = _eventService.createEvent(jwt, eventDto);
        if (newEvent == null)
        {
            return Conflict("Conflict");
        }
        else
        {
            return Ok(newEvent);
        }
    }
    [HttpGet]
    public IActionResult GetEvent([FromQuery] Guid eventId)
    {
        var foundEvent = _eventService.getEventById(eventId);
        if (foundEvent == null)
        {
            return Conflict("Event was not found");
        }
        return Ok(foundEvent);
    }
}