using System.Threading.Tasks;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Services
{
    public interface IOmdbMovieService
    {
        Task<Movie> SearchByTitle(string title);
    }
}