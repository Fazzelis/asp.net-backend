using restApi.Dtos.Event;
using restApi.Entity;

namespace restApi.Mappers;

public static class EventMapper
{
    public static EventGetDto toEventGetDto(this Event eventModel)
    {
        return new EventGetDto
        {
            Title = eventModel.Title,
            Description = eventModel.Description,
            StartTime = eventModel.StartTime,
            EndTime = eventModel.EndTime,
            PlaceOfEvent = eventModel.PlaceOfEvent,
            CreatorName = eventModel.Author.Name
        };
    }
    public static Event toEvent(this EventCreateDto eventCreateDto, User user)
    {
        return new Event
        {
            Title = eventCreateDto.Title,
            Description = eventCreateDto.Description,
            StartTime = eventCreateDto.StartTime,
            EndTime = eventCreateDto.EndTime,
            PlaceOfEvent = eventCreateDto.PlaceOfEvent,
            Author = user
        };
    }
}