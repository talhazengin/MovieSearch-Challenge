using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Services;

namespace MovieSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieSearchController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOmdbMovieSearchService _omdbMovieSearchService;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public MovieSearchController(IDistributedCache distributedCache, IOmdbMovieSearchService omdbMovieSearchService)
        {
            _distributedCache = distributedCache;
            _omdbMovieSearchService = omdbMovieSearchService;

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

            movieInfoModel = await _omdbMovieSearchService.SearchByTitle(title);

            await _distributedCache.SetAsync(movieInfoModel.ImdbId, movieInfoModel, _distributedCacheEntryOptions);
            await _distributedCache.SetAsync(movieInfoModel.Title, movieInfoModel, _distributedCacheEntryOptions);

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

            movieInfoModel = await _omdbMovieSearchService.SearchByImdbId(imdbId);

            await _distributedCache.SetAsync(movieInfoModel.ImdbId, movieInfoModel, _distributedCacheEntryOptions);
            await _distributedCache.SetAsync(movieInfoModel.Title, movieInfoModel, _distributedCacheEntryOptions);

            return movieInfoModel;
        }
    }
}
