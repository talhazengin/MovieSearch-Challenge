using System.Linq;
using System.Threading.Tasks;
using MovieSearch.Core.Exceptions;
using MovieSearch.Data.DAL;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Data.QueryProcessors
{
    public class MovieInfoQueryProcessor : IMovieInfoQueryProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieInfoQueryProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<MovieInfo> GetAllMovieInfos()
        {
            return _unitOfWork.Query<MovieInfo>();
        }

        public MovieInfo GetMovieInfoById(int id)
        {
            MovieInfo movieInfo = GetAllMovieInfos().FirstOrDefault(m => m.Id == id);

            if (movieInfo == null)
            {
                throw new NotFoundException("Movie Info was not found!");
            }

            return movieInfo;
        }

        public async Task CreateMovieInfo(MovieInfoModel movieInfoModel)
        {
            // There should not be same imdbId in the system.
            // So check for any movie info saved with same imdbId before.
            bool isThereAnyMovieInfoWithSameImdbId = GetAllMovieInfos().Any(m => m.ImdbId == movieInfoModel.ImdbId);

            if (isThereAnyMovieInfoWithSameImdbId)
            {
                throw new BadRequestException("A movie info with the same ImdbId created before!");
            }

            var movieInfo = new MovieInfo
            {
                ImdbId = movieInfoModel.ImdbId,
                Title = movieInfoModel.Title,
                MovieInfoJson = movieInfoModel.MovieInfoJson
            };

            _unitOfWork.Add(movieInfo);

            await _unitOfWork.CommitAsync();
        }

        public Task UpdateMovieInfo(int id, MovieInfoModel movieInfoModel)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMovieInfo(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}