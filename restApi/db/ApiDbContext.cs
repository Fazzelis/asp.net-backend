using Microsoft.EntityFrameworkCore;
using restApi.Entity;

namespace restApi.db;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<JwtToken> JwtTokens { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Event> Events { get; set; }
}