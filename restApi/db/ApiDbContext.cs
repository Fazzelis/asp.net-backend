using Microsoft.EntityFrameworkCore;
using restApi.Models;

namespace restApi.db;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<JwtToken> JwtTokens { get; set; }
}