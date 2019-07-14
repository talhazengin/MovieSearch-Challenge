using System.Linq;
using System.Threading.Tasks;
using MovieSearch.Core.Exceptions;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Data.QueryProcessors
{
    public interface IMovieInfoQueryProcessor
    {
        /// <summary>
        /// Gets all the movie infos that stored in the system.
        /// </summary>
        /// <returns>Queryable list of movie infos</returns>
        IQueryable<MovieInfo> GetAllMovieInfos();

        /// <summary>
        /// Gets a specific movie info by the given id.
        /// </summary>
        /// <param name="id">Id of movie info</param>
        /// <exception cref="NotFoundException"></exception>
        MovieInfo GetMovieInfoById(int id);

        /// <summary>
        /// Creates a new movie info in the database.
        /// </summary>
        /// <param name="movieInfoModel">Create model of the movie info.</param>
        /// <exception cref="BadRequestException"></exception>
        Task CreateMovieInfo(MovieInfoModel movieInfoModel);

        /// <summary>
        /// Updates the movie info with the given id and new model.
        /// </summary>
        /// <param name="id">Id of movie info</param>
        /// <param name="movieInfoModel">Update model of the movie info.</param>
        Task UpdateMovieInfo(int id, MovieInfoModel movieInfoModel);

        /// <summary>
        /// Deletes the movie info by the given id.
        /// </summary>
        /// <param name="id">Id of movie info</param>
        Task DeleteMovieInfo(int id);
    }
}
