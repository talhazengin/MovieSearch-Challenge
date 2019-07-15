using System;
using System.Threading;
using System.Threading.Tasks;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Data.QueryProcessors;

namespace MovieSearch.Services
{
    public class MovieDbUpdateService : IMovieDbUpdateService
    {
        private readonly IOmdbMovieSearchService _omdbMovieSearchService;
        private readonly IMovieInfoQueryProcessor _movieInfoQueryProcessor;

        public MovieDbUpdateService(IOmdbMovieSearchService omdbMovieSearchService, IMovieInfoQueryProcessor movieInfoQueryProcessor)
        {
            _omdbMovieSearchService = omdbMovieSearchService;
            _movieInfoQueryProcessor = movieInfoQueryProcessor;
        }

        public void StartBackgroundUpdateProcess()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    UpdateMovieDatabase();
                }
                catch (Exception)
                {
                    // TODO: LOG
                }

                await Task.Delay(TimeSpan.FromMinutes(10));

            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async void UpdateMovieDatabase()
        {
            foreach (MovieInfo movieInfo in _movieInfoQueryProcessor.GetAllMovieInfos())
            {
                // This search automatically creates database record. Don't need to write to database explicitly.
                await _omdbMovieSearchService.SearchByImdbId(movieInfo.ImdbId, false);
            }
        }
    }
}