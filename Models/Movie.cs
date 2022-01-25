using System.Linq;

namespace RottenPotatoes.Models
{
  public interface IMovieRepository
  {
    IQueryable<Movie> Movies { get; }
  }

  public class Movie
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
  public class EFMovieRepository : IMovieRepository
  {
    private AppDbContext _applicationDbContext;
    public EFMovieRepository(AppDbContext applicationDbContext)
    {
      _applicationDbContext = applicationDbContext;
    }
    public IQueryable<Movie> Movies => _applicationDbContext.Movies;
  }
}