namespace rottenpotatoes.Models
{
  public class MovieDetailsModel
  {
    public int MovieId { get; set; } = 1;
    public Movie Movie { get; set; }
    public Description Description { get; set; }
    public decimal Score { get; set; } = 0;
    public decimal VotesCount { get; set; } = 0;
  }
}
