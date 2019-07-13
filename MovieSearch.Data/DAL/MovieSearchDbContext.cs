using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MovieSearch.Data.Maps;

namespace MovieSearch.Data.DAL
{
    public class MovieSearchDbContext : DbContext
    {
        public MovieSearchDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMap mapping in GetMappings())
            {
                mapping.Visit(modelBuilder);
            }
        }

        private static IEnumerable<IMap> GetMappings()
        {
            IEnumerable<TypeInfo> assemblyTypes = typeof(UserMap).Assembly.DefinedTypes;

            IEnumerable<TypeInfo> mappings = assemblyTypes
                .Where(typeInfo => typeof(IMap).IsAssignableFrom(typeInfo) && !typeInfo.IsAbstract);

            return mappings.Select(mapping => (IMap)Activator.CreateInstance(mapping.AsType()));
        }
    }
}
