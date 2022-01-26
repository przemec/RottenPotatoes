
using System;
using RottenPotatoes.Models;

namespace rottenpotatoes
{
  public class SeedData
  {
    private AppDbContext _dbContext;
    public SeedData(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async void EnsurePopulated()
    {
      if (await _dbContext.Movies.FindAsync(1) == null)
      {
        await _dbContext.Movies.AddRangeAsync(default_movies);
        await _dbContext.SaveChangesAsync();
      }
      if (await _dbContext.Descriptions.FindAsync(1) == null)
      {
        await _dbContext.Descriptions.AddRangeAsync(default_descriptions);
        await _dbContext.SaveChangesAsync();
      }
      if (await _dbContext.UserVotes.FindAsync(1) == null)
      {
        await _dbContext.UserVotes.AddRangeAsync(default_user_votes);
        await _dbContext.SaveChangesAsync();
      }
    }
    private Movie[] default_movies = new Movie[] {
      new Movie(
        "Black Widow",
        "Cate Shortland",
        "Kevin Feige",
        "images\\BlackWidow.jpg",
        "Adventure, Action",
        "2h 13m"
      ),
      new Movie(
        "The Green Knight",
        "David Lowery",
        "Tim Headington, Toby Halbrooks, James M. Johnston, Theresa Steele Page",
        "images\\TheGreenKnight.jpg",
        "Adventure, Fantasy",
        "2h 5m"
      ),
      new Movie(
        "Pig",
        "Michael Sarnoski",
        "Vanessa Block, Dimitra Tsingou, Thomas Benski, Ben Giladi, Dori A. Rath, Joseph Restaino, David Carrico, Adam Paulsen, Steve Tisch, Nicolas Cage",
        "images\\Pig.jpg",
        "Mystery & Thriller, Drama",
        "1h 32m"
      ),
      new Movie(
        "Nobody",
        "Ilya Naishuller",
        "Kelly McCormick, David Leitch, Braden Aftergood, Bob Odenkirk, Marc Provissiero",
        "images\\Nobody.jpg",
        "Comedy, Action, Mystery & Thriller",
        "1h 32m"
      ),
      new Movie(
        "Dune",
        "Denis Villeneuve",
        "Denis Villeneuve, Mary Parent, Cale Boyter, Joseph Caracciolo Jr.",
        "images\\Dune.jpg",
        "Adventure, Sci-Fi",
        "2h 13m"
      ),
      new Movie(
        "Don't look up",
        "Adam McKay",
        "Adam McKay, Kevin J. Messick",
        "images\\DontLookUp.jpg",
        "Comedy",
        "2h 35m"
      ),
    };
    private Description[] default_descriptions = new Description[] {
      new Description(1, "Natasha Romanoff, aka Black Widow, confronts the darker parts of her ledger when a dangerous conspiracy with ties to her past arises. Pursued by a force that will stop at nothing to bring her down, Natasha must deal with her history as a spy, and the broken relationships left in her wake long before she became an Avenger."),
      new Description(2, "An epic fantasy adventure based on the timeless Arthurian legend, THE GREEN KNIGHT tells the story of Sir Gawain (Dev Patel), King Arthur's reckless and headstrong nephew, who embarks on a daring quest to confront the eponymous Green Knight, a gigantic emerald-skinned stranger and tester of men. Gawain contends with ghosts, giants, thieves, and schemers in what becomes a deeper journey to define his character and prove his worth in the eyes of his family and kingdom by facing the ultimate challenger. From visionary filmmaker David Lowery comes a fresh and bold spin on a classic tale from the knights of the round table."),
      new Description(3, "Living alone in the Oregon wilderness, a truffle hunter returns to Portland to find the person who stole his beloved pig."),
      new Description(4, "Emmy winner Bob Odenkirk (Better Call Saul, The Post, Nebraska) stars as Hutch Mansell, an underestimated and overlooked dad and husband, taking life's indignities on the chin and never pushing back. A nobody. When two thieves break into his suburban home one night, Hutch declines to defend himself or his family, hoping to prevent serious violence. His teenage son, Blake (Gage Munroe, The Shack), is disappointed in him and his wife, Becca (Connie Nielsen, Wonder Woman), seems to pull only further away. The aftermath of the incident strikes a match to Hutch's long-simmering rage, triggering dormant instincts and propelling him on a brutal path that will surface dark secrets and lethal skills. In a barrage of fists, gunfire and squealing tires, Hutch must save his family from a dangerous adversary (famed Russian actor Aleksey Serebryakov, Amazon's McMafia)--and ensure that he will never be underestimated as a nobody again."),
      new Description(5, "Paul Atreides, a brilliant and gifted young man born into a great destiny beyond his understanding, must travel to the most dangerous planet in the universe to ensure the future of his family and his people. As malevolent forces explode into conflict over the planet's exclusive supply of the most precious resource in existence, only those who can conquer their own fear will survive."),
      new Description(6, "Kate Dibiasky (Jennifer Lawrence), an astronomy grad student, and her professor Dr. Randall Mindy (Leonardo DiCaprio) make an astounding discovery of a comet orbiting within the solar system. The problem: it's on a direct collision course with Earth. The other problem? No one really seems to care. Turns out warning mankind about a planet-killer the size of Mount Everest is an inconvenient fact to navigate. With the help of Dr. Oglethorpe (Rob Morgan), Kate and Randall embark on a media tour that takes them from the office of an indifferent President Orlean (Meryl Streep) and her sycophantic son and Chief of Staff, Jason (Jonah Hill), to the airwaves of The Daily Rip, an upbeat morning show hosted by Brie (Cate Blanchett) and Jack (Tyler Perry). With only six months until the comet makes impact, managing the 24-hour news cycle and gaining the attention of the social media obsessed public before it's too late proves shockingly comical -- what will it take to get the world to just look up?!"),
    };
    private UserVote[] default_user_votes = new UserVote[] {
      new UserVote("user1", 2, 5),
      new UserVote("user2", 2, 5),
      new UserVote("user2", 3, 4),
      new UserVote("user3", 1, 3),
      new UserVote("user3", 2, 5),
      new UserVote("user3", 3, 4),
      new UserVote("user3", 5, 4),
      new UserVote("user4", 2, 5),
      new UserVote("user4", 3, 4),
      new UserVote("user4", 6, 2),
      new UserVote("user5", 1, 5),
      new UserVote("user5", 2, 4),
    };
  }
}