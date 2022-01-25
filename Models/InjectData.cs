
using System;
using RottenPotatoes.Models;

namespace rottenpotatoes
{
  public class SeedData
  {
    private static Movie[] default_movies = new Movie[] {
      new Movie("Black Widow", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
      new Movie("Black Widow2", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
      new Movie("Black Widow3", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
      new Movie("Black Widow4", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
      new Movie("Black Widow5", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
      new Movie("Black Widow6", "Cate Shortland", "Kevin Feige", "---", "Adventure, Action", "2h 13m"),
    };
    private static UserVote[] default_user_votes = new UserVote[] {
      new UserVote("user1", 3, 4),
      new UserVote("user1", 3, 4),
      new UserVote("user2", 3, 4),
      new UserVote("user3", 3, 4),
      new UserVote("user3", 3, 4),
    };
    private AppDbContext _dbContext;
    public SeedData(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async void EnsurePopulated()
    {
      // Movie m = await _dbContext.Movies.FindAsync(0);
      // Console.WriteLine("m: ");
      // Console.WriteLine(m);
      // Console.WriteLine(await _dbContext.Movies.FindAsync(0));
      if (await _dbContext.Movies.FindAsync(0) == null)
      {
        await _dbContext.Movies.AddRangeAsync(default_movies);
        await _dbContext.SaveChangesAsync();
      }
      if (await _dbContext.UserVotes.FindAsync(0) == null)
      {
        await _dbContext.UserVotes.AddRangeAsync(default_user_votes);
        await _dbContext.SaveChangesAsync();
      }
    }
  }
}