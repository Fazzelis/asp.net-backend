using restApi.db;
using restApi.Dtos.Event;
using restApi.Mappers;
using restApi.Entity;

namespace restApi.Services.Impl;

public class EventService : IEventService
{
    private readonly ApiDbContext _context;
    public EventService(ApiDbContext context)
    {
        _context = context;
    }
    public Event? createEvent(string jwt, EventCreateDto eventDto)
    {
        var jwtToken = _context.JwtTokens.FirstOrDefault(token => token.Token == jwt);
        if (jwtToken == null)
        {
            return null;
        }
        if (DateTime.UtcNow > jwtToken.ExpirationTime)
        {
            _context.JwtTokens.Remove(jwtToken);
            _context.SaveChanges();
            return null;
        }
        var user = _context.Users.FirstOrDefault(u => u.Id == jwtToken.UserId);
        if (user == null)
        {
            return null;
        }
        if (user.Role == "admin" || user.Role == "writer")
        {
            Event db_event = new Event{
                Title=eventDto.Title,
                Description=eventDto.Description,
                StartTime=eventDto.StartTime,
                EndTime=eventDto.EndTime,
                PlaceOfEvent=eventDto.PlaceOfEvent,
                CreatorName=user.Name,
                CreatorId=user.Id
            };
            _context.Events.Add(db_event);
            _context.SaveChanges();
            return db_event;
        }
        else
        {
            return null;
        }
    }
    public EventGetDto? getEventById(Guid eventId)
    {
        var foundEvent = _context.Events.FirstOrDefault(e => e.Id == eventId);
        if (foundEvent == null)
        {
            return  null;
        }
        return foundEvent.toEventGetDto();
    }
}