using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Management_System.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public string Ticket { get; set; }
        public string Country { get; set; }
        // the movie should have more than one Genre.
        public ICollection<Genre> Genres{ get; set; }
        public int? GenreId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
