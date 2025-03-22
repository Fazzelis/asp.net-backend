namespace restApi.Entity;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public JwtToken? JwtToken { get; set; }
    public ICollection<News>? News { get; set; }
    public ICollection<Event>? Events { get; set; }
}
