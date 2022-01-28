using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rottenpotatoes.Data;

namespace rottenpotatoes.Controllers
{
  [ApiController]
  [Route("Api/")]
  public class RottenApiController : ControllerBase
  {
    private readonly AppDbContext _dbContext;
    public RottenApiController(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpGet("ListTitles")]
    public ActionResult<List<string>> ListTitles()
    {
      List<string> response = new List<string>();
      foreach (var movie in _dbContext.Movies)
      {
        response.Add(movie.Title);
      }
      return CreatedAtAction("ListTitles", response);
    }

    public class MovieWithId
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Director { get; set; }
      public MovieWithId(int id, string title, string director)
      {
        Id = id;
        Title = title;
        Director = director;
      }
    }
    [HttpGet("GetTitlesWithIds")]
    public ActionResult<List<MovieWithId>> GetTitlesWithIds()
    {
      List<MovieWithId> response = new List<MovieWithId>();
      foreach (var movie in _dbContext.Movies)
      {
        response.Add(new MovieWithId(movie.MovieId, movie.Title, movie.Director));
      }
      return CreatedAtAction("GetTitlesWithIds", response);
    }

    public class MovieWithVotes
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public decimal Score { get; set; }
      public decimal Votes_count { get; set; }
      public MovieWithVotes(int id, string title, decimal score, decimal votescount)
      {
        Id = id;
        Title = title;
        Score = score;
        Votes_count = votescount;
      }
    }
    [HttpGet("GetTitlesWithVotes")]
    public ActionResult<List<MovieWithVotes>> GetTitlesWithVotes()
    {
      List<MovieWithVotes> response = new List<MovieWithVotes>();
      var movies = _dbContext.Movies.ToList();
      var uservotes = _dbContext.UserVotes.ToList();
      foreach (var movie in movies)
      {
        decimal score = 0;
        decimal votes_count = 0;
        int movieid = movie.MovieId;
        foreach (var vote in uservotes)
        {
          if (vote.MovieId == movieid)
          {
            score = score == 0 ? vote.Score : ((score * (votes_count) + vote.Score) / (votes_count + 1));
            votes_count += 1;
          }
        }
        response.Add(new MovieWithVotes(movieid, movie.Title, score, votes_count));
      }
      return CreatedAtAction("GetTitlesWithVotes", response);
    }

    public class MovieDetails
    {
      public string Title { get; set; }
      public string Director { get; set; }
      public string Producer { get; set; }
      public string Genre { get; set; }
      public string Runtime { get; set; }
      public MovieDetails(string title, string director, string producer, string genre, string runtime)
      {
        Title = title;
        Director = director;
        Producer = producer;
        Genre = genre;
        Runtime = runtime;
      }
    }
    [HttpGet("GetMovieDetailsById")]
    public ActionResult<MovieDetails> GetMovieDetailsById(int id)
    {
      var mov = _dbContext.Movies.Find(id);
      MovieDetails response = new MovieDetails(
        mov.Title,
        mov.Director,
        mov.Producer,
        mov.Genre,
        mov.Runtime
      );
      return CreatedAtAction("GetMovieDetailsById", response);
    }

    public class MovieDescription
    {
      public string Title { get; set; }
      public string Description { get; set; }
      public MovieDescription(string title, string description)
      {
        Title = title;
        Description = description;
      }
    }
    [HttpGet("GetMovieDescriptionById")]
    public ActionResult<MovieDescription> GetMovieDescriptionById(int id)
    {
      string mov = _dbContext.Movies.Find(id).Title;
      string desc = _dbContext.Descriptions.SingleOrDefault(d => d.MovieId == id).Desc;
      MovieDescription response = new MovieDescription(
        mov,
        desc
      );
      return CreatedAtAction("GetMovieDescriptionById", response);
    }
  }
}