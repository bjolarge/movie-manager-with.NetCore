using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Models;

namespace Movie_Management_System.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } 
        public DbSet<Genre> Genres { get; set; } 

    }
}
