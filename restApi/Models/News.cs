namespace restApi.Models;

public class News
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
    public required DateTime PublicTime { get; set; }
    public required Guid AuthorId { get; set; }
}
