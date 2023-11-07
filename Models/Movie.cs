using System.ComponentModel.DataAnnotations.Schema;
using Movie_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

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


public static class MovieEndpoints
{
	public static void MapMovieEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Movie").WithTags(nameof(Movie));

        group.MapGet("/", async (MovieContext db) =>
        {
            return await db.Movies.ToListAsync();
        })
        .WithName("GetAllMovies")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Movie>, NotFound>> (int id, MovieContext db) =>
        {
            return await db.Movies.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Movie model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMovieById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Movie movie, MovieContext db) =>
        {
            var affected = await db.Movies
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, movie.Id)
                  .SetProperty(m => m.Name, movie.Name)
                  .SetProperty(m => m.Description, movie.Description)
                  .SetProperty(m => m.ReleaseDate, movie.ReleaseDate)
                  .SetProperty(m => m.Rating, movie.Rating)
                  .SetProperty(m => m.Ticket, movie.Ticket)
                  .SetProperty(m => m.Country, movie.Country)
                  .SetProperty(m => m.GenreId, movie.GenreId)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMovie")
        .WithOpenApi();

        group.MapPost("/", async (Movie movie, MovieContext db) =>
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Movie/{movie.Id}",movie);
        })
        .WithName("CreateMovie")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MovieContext db) =>
        {
            var affected = await db.Movies
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMovie")
        .WithOpenApi();
    }
}}
