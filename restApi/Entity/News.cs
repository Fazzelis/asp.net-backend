namespace restApi.Entity;

public class News
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime PublicTime { get; set; }
    public required User Author { get; set; }
}
