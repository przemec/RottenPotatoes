using System.Collections.Generic;
using System.Linq;

namespace RottenPotatoes.Models
{
  public class Movie
  {
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Runtime { get; set; }
    public List<UserVote> UserVotes { get; set; }
    public Movie() { }
    public Movie(string title, string director, string producer, string description, string genre, string runtime)
    {
      Title = title;
      Director = director;
      Producer = producer;
      Description = description;
      Genre = genre;
      Runtime = runtime;
    }
  }

  public class UserVote
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public int MovieId { get; set; }
    public int Score { get; set; }
    public Movie Movie { get; set; }
    public UserVote() { }
    public UserVote(string username, int movieid, int score)
    {
      UserName = username;
      MovieId = movieid;
      Score = score;
    }
  }
  public interface IMovieRepository
  {
    IQueryable<Movie> Movies { get; }
    IQueryable<UserVote> UserVotes { get; }
  }


  public class EFMovieRepository : IMovieRepository
  {
    private AppDbContext _applicationDbContext;
    public EFMovieRepository(AppDbContext applicationDbContext)
    {
      _applicationDbContext = applicationDbContext;
    }
    public IQueryable<Movie> Movies => _applicationDbContext.Movies;
    public IQueryable<UserVote> UserVotes => _applicationDbContext.UserVotes;
  }
}