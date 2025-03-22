namespace restApi.Entity;

public class JwtToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpirationTime { get; set; }
}