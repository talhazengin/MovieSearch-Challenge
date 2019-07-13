using Microsoft.EntityFrameworkCore;
using MovieSearch.Data.Models.User;

namespace MovieSearch.Data.Maps
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("User")
                .HasKey(user => user.Id);
        }
    }
}