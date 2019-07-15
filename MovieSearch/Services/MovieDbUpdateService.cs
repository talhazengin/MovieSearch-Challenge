using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MovieSearch.Core.Services;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Data.QueryProcessors;

namespace MovieSearch.Services
{
    public class MovieDbUpdateService : IMovieDbUpdateService
    {
        private readonly IMovieSearchService _movieSearchService;
        private readonly IMovieInfoQueryProcessor _movieInfoQueryProcessor;
        private readonly ILogger<MovieDbUpdateService> _logger;

        public MovieDbUpdateService(IMovieSearchService movieSearchService, IMovieInfoQueryProcessor movieInfoQueryProcessor, ILogger<MovieDbUpdateService> logger)
        {
            _movieSearchService = movieSearchService;
            _movieInfoQueryProcessor = movieInfoQueryProcessor;
            _logger = logger;
        }

        public void StartBackgroundUpdateProcess()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    UpdateMovieDatabase();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Updating movie database error!");
                }

                await Task.Delay(TimeSpan.FromMinutes(10));

            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async void UpdateMovieDatabase()
        {
            foreach (MovieInfo movieInfo in _movieInfoQueryProcessor.GetAllMovieInfos())
            {
                MovieInfoModel movieInfoModel = await _movieSearchService.SearchByImdbId(movieInfo.ImdbId, false);

                await _movieInfoQueryProcessor.UpdateMovieInfo(movieInfo.Id, movieInfoModel);
            }
        }
    }
}