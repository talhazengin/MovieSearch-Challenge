using System.Threading.Tasks;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Services
{
    public interface IOmdbMovieSearchService
    {
        Task<MovieInfoModel> SearchByTitle(string title);

        Task<MovieInfoModel> SearchByImdbId(string imdbId);
    }
}