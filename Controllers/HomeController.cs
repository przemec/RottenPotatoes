﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rottenpotatoes.Models;

namespace rottenpotatoes.Controllers
{
  public class HomeController : Controller
  {
    private readonly AppDbContext _dbContext;
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
      _logger = logger;
      _dbContext = dbContext;
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
        Description = await _dbContext.Descriptions.FindAsync(movieid),
        Score = score,
        VotesCount = votes_count,
      });
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
