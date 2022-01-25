using Microsoft.EntityFrameworkCore;

namespace RottenPotatoes.Models
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Movie> Movies { get; set; }
  }
}