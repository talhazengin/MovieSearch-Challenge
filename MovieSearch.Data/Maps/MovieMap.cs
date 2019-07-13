using Microsoft.EntityFrameworkCore;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Data.Maps
{
    public class MovieMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Movie>()
                .ToTable("Movie")
                .HasKey(movie => movie.Id);
        }
    }
}