using Microsoft.EntityFrameworkCore;

namespace MovieSearch.Data.Maps
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}