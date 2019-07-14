using Microsoft.EntityFrameworkCore;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Data.Maps
{
    public class MovieInfoMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<MovieInfo>()
                .ToTable("MovieInfo")
                .HasKey(movieInfo => movieInfo.Id);
        }
    }
}