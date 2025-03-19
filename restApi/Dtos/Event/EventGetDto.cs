namespace restApi.Dtos.Event;

public class EventGetDto
{
    public required string Title {get;set;}
    public required string Description {get;set;}
    public required DateTime StartTime {get;set;}
    public required DateTime EndTime {get;set;}
    public required string PlaceOfEvent {get;set;}
    public required string CreatorName {get;set;}
}