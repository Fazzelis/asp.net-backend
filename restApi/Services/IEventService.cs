using restApi.Dtos.Event;
using restApi.Models;

namespace restApi.Services;

public interface IEventService
{
    public Event? createEvent(string jwt, EventDto eventDto);
}