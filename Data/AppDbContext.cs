using Microsoft.EntityFrameworkCore;
using rottenpotatoes.Models;

namespace rottenpotatoes.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserVote> UserVotes { get; set; }
    public DbSet<Description> Descriptions { get; set; }
  }
}