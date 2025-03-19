namespace restApi.Dtos.Event;

public class EventCreateDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required string PlaceOfEvent { get; set; }
}