using restApi.Dtos.Event;
using restApi.Entity;

namespace restApi.Mappers;

public static class EventMapper
{
    public static EventGetDto toEventGetDto(this Event eventModel)
    {
        return new EventGetDto{
            Title=eventModel.Title,
            Description=eventModel.Description,
            StartTime=eventModel.StartTime,
            EndTime=eventModel.EndTime,
            PlaceOfEvent=eventModel.PlaceOfEvent,
            CreatorName=eventModel.CreatorName
        };
    }
}