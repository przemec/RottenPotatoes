using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rottenpotatoes.Models;
using rottenpotatoes.Data;
using Microsoft.AspNetCore.Identity;

namespace rottenpotatoes.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;
    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _logger = logger;
      _dbContext = dbContext;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public IActionResult Index()
    {
      dynamic mymodel = new ExpandoObject();
      mymodel.Movies = _dbContext.Movies;
      Dictionary<int, decimal[]> votes = new Dictionary<int, decimal[]>();
      foreach (var vote in _dbContext.UserVotes)
      {
        if (!votes.ContainsKey(vote.MovieId))
        {
          decimal[] v = new decimal[] { vote.Score, 1 };
          votes.Add(vote.MovieId, v);
        }
        else
        {
          decimal[] existing = votes[vote.MovieId];
          decimal[] v = new decimal[] { ((existing[0] * (existing[1]) + vote.Score) / (existing[1] + 1)), (existing[1] + 1) };
          votes[vote.MovieId] = v;
        }
      }
      mymodel.Votes = votes;
      return View(mymodel);
    }

    public async Task<IActionResult> MovieDetails(int movieid)
    {
      decimal score = 0;
      decimal votes_count = 0;
      string current_user_name;
      int user_score = 0;
      if (_signInManager.IsSignedIn(User))
      {
        current_user_name = User.Identity.Name;
        user_score = _dbContext.UserVotes.SingleOrDefault(uv => (uv.MovieId == movieid && uv.UserName == current_user_name))?.Score ?? 0;
      }
      foreach (var vote in _dbContext.UserVotes)
      {
        if (vote.MovieId == movieid)
        {
          score = score == 0 ? vote.Score : ((score * (votes_count) + vote.Score) / (votes_count + 1));
          votes_count += 1;
        }
      }
      return View(new MovieDetailsModel
      {
        MovieId = movieid,
        Movie = await _dbContext.Movies.FindAsync(movieid),
        Description = _dbContext.Descriptions.SingleOrDefault(d => d.MovieId == movieid),
        Score = score,
        VotesCount = votes_count,
        UserScore = user_score,
        HasUserVoted = user_score != 0
      });
    }

    [HttpGet]
    public IActionResult AddMovie()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddMovie(MovieAddModel movieAddModel)
    {
      if (ModelState.IsValid)
      {
        Movie movie = new Movie(
          movieAddModel.Title,
          movieAddModel.Director,
          movieAddModel.Producer,
          movieAddModel.ImageSrc,
          movieAddModel.Genre,
          movieAddModel.Runtime
        );
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
        var new_movie_id = _dbContext.Movies.OrderBy(m => m.MovieId).Last(m => m.Title == movieAddModel.Title).MovieId;
        Description description = new Description(
          new_movie_id,
          movieAddModel.Desc
        );
        await _dbContext.Descriptions.AddAsync(description);
        await _dbContext.SaveChangesAsync();
        return Redirect("/");
      }
      return View(movieAddModel);
    }

    public async Task<IActionResult> RemoveMovie(int movieid)
    {
      var movie_to_delete = _dbContext.Movies.First(m => m.MovieId == movieid);
      _dbContext.Movies.Remove(movie_to_delete);
      await _dbContext.SaveChangesAsync();
      return Redirect("/");
    }

    [HttpGet]
    public IActionResult EditMovie(int movieid)
    {
      var movie_to_edit = _dbContext.Movies.First(m => m.MovieId == movieid);
      var desc_to_edit = _dbContext.Descriptions.First(m => m.MovieId == movieid);
      MovieEditModel movie = new MovieEditModel{
        MovieId = movieid,
        MovieDescId = desc_to_edit.Id,
        Title = movie_to_edit.Title,
        Director = movie_to_edit.Director,
        Producer = movie_to_edit.Producer,
        ImageSrc = movie_to_edit.ImageSrc,
        Genre = movie_to_edit.Genre,
        Runtime = movie_to_edit.Runtime,
        Desc = desc_to_edit.Desc,
      };
      return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> EditMovie(MovieEditModel movieEditModel)
    {
      if (ModelState.IsValid)
      {
        Movie movie = new Movie(
          movieEditModel.MovieId,
          movieEditModel.Title,
          movieEditModel.Director,
          movieEditModel.Producer,
          movieEditModel.ImageSrc,
          movieEditModel.Genre,
          movieEditModel.Runtime
        );
        _dbContext.Movies.Update(movie);
        Description description = new Description(
          movieEditModel.MovieDescId,
          movieEditModel.MovieId,
          movieEditModel.Desc
        );
        _dbContext.Descriptions.Update(description);
        await _dbContext.SaveChangesAsync();
        return Redirect($"/Home/MovieDetails?movieid={movieEditModel.MovieId}");
      }
      return View(movieEditModel);
    }

    public async Task<IActionResult> AddVote(int uservote, int movieid, string redirectUrl = "/")
    {
      if (uservote >= 1 && uservote <= 5)
      {
        UserVote new_vote = new UserVote(User.Identity.Name, movieid, uservote);
        await _dbContext.UserVotes.AddAsync(new_vote);
        await _dbContext.SaveChangesAsync();
      }
      return Redirect(redirectUrl);
    }

    public Task<IActionResult> ChangeVote(int uservote, int movieid, string redirectUrl = "/")
    {
      if (uservote >= 1 && uservote <= 5)
      {
        var vote_to_delete = _dbContext.UserVotes.First(uv => (uv.MovieId == movieid && uv.UserName == User.Identity.Name));
        _dbContext.UserVotes.Remove(vote_to_delete);
      }
      return AddVote(uservote, movieid, redirectUrl);
    }

    public async Task<IActionResult> RemoveVote(int movieid)
    {
      var vote_to_delete = _dbContext.UserVotes.First(uv => (uv.MovieId == movieid && uv.UserName == User.Identity.Name));
      _dbContext.UserVotes.Remove(vote_to_delete);
      await _dbContext.SaveChangesAsync();
      return Redirect($"/Home/MovieDetails?movieid={movieid}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
