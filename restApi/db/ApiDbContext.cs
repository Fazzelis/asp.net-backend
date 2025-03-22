using Microsoft.EntityFrameworkCore;
using restApi.Entity;

namespace restApi.db;

public class ApiDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<JwtToken> JwtTokens { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Event> Events { get; set; }
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.JwtToken)
            .WithOne(j => j.User)
            .HasForeignKey<JwtToken>(j => j.UserId);
    }
}