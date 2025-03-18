namespace restApi.Dtos.News;

public class NewsGetDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
}