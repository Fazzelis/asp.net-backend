namespace restApi.Models;

public class Event
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
}