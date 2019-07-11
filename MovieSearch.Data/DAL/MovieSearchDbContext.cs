using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MovieSearch.Data.Maps;

namespace MovieSearch.Data.DAL
{
    public class MovieSearchDbContext : DbContext
    {
        public MovieSearchDbContext(DbContextOptions<MovieSearchDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<IMap> mappings = MappingsHelper.GetMappings();

            foreach (IMap mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }
        }
    }
}
