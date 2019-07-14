using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Services;

namespace MovieSearch.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class MovieSearchController : ControllerBase
    {
        private readonly IOmdbMovieSearchService _omdbMovieSearchService;

        public MovieSearchController(IOmdbMovieSearchService omdbMovieSearchService)
        {
            _omdbMovieSearchService = omdbMovieSearchService;
        }

        [HttpGet("search", Name = "{title}")]
        public async Task<MovieInfoModel> SearchByTitle(string title)
        {
            return await _omdbMovieSearchService.SearchByTitle(title);
        }
    }
}
