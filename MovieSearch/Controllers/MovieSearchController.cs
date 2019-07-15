using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Data.QueryProcessors;
using MovieSearch.Services;

namespace MovieSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieSearchController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMovieSearchService _movieSearchService;
        private readonly IMovieInfoQueryProcessor _movieInfoQueryProcessor;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public MovieSearchController(IDistributedCache distributedCache, IMovieSearchService movieSearchService, IMovieInfoQueryProcessor movieInfoQueryProcessor)
        {
            _distributedCache = distributedCache;
            _movieSearchService = movieSearchService;
            _movieInfoQueryProcessor = movieInfoQueryProcessor;

            _distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(12) // 12 Minutes caching.
            };
        }

        [HttpGet("title", Name = "{title}")]
        public async Task<MovieInfoModel> SearchByTitle(string title)
        {
            MovieInfoModel movieInfoModel = await _distributedCache.GetAsync<MovieInfoModel>(title);

            if (movieInfoModel != null)
            {
                return movieInfoModel;
            }

            movieInfoModel = await _movieSearchService.SearchByTitle(title, true);

            await _distributedCache.SetAsync(movieInfoModel.ImdbId, movieInfoModel, _distributedCacheEntryOptions);
            await _distributedCache.SetAsync(movieInfoModel.Title, movieInfoModel, _distributedCacheEntryOptions);

            // Add found movie info to the database.
            await _movieInfoQueryProcessor.CreateMovieInfo(movieInfoModel);

            return movieInfoModel;
        }

        [HttpGet("imdb", Name = "{imdb}")]
        public async Task<MovieInfoModel> SearchByImdbId(string imdbId)
        {
            MovieInfoModel movieInfoModel = await _distributedCache.GetAsync<MovieInfoModel>(imdbId);

            if (movieInfoModel != null)
            {
                return movieInfoModel;
            }

            movieInfoModel = await _movieSearchService.SearchByImdbId(imdbId, true);

            await _distributedCache.SetAsync(movieInfoModel.ImdbId, movieInfoModel, _distributedCacheEntryOptions);
            await _distributedCache.SetAsync(movieInfoModel.Title, movieInfoModel, _distributedCacheEntryOptions);

            // Add found movie info to the database.
            await _movieInfoQueryProcessor.CreateMovieInfo(movieInfoModel);

            return movieInfoModel;
        }
    }
}
