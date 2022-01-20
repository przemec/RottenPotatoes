using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RottenPotatoes.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Movie> Movies { get; set; }
    }

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
        private ApplicationDbContext _applicationDbContext;
        public EFMovieRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<Movie> Movies => _applicationDbContext.Movies;
    }

}