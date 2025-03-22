namespace restApi.Entity;

public class JwtToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
}