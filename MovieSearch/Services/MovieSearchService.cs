using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MovieSearch.Core.Exceptions;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Data.QueryProcessors;
using Newtonsoft.Json.Linq;

namespace MovieSearch.Services
{
    public class MovieSearchService : IMovieSearchService
    {
        private const string OmdbApiAuthKey = "fa904133";

        private static readonly Uri OmdbApiEndPointUri = new Uri("http://www.omdbapi.com/", UriKind.Absolute);

        private readonly IMovieInfoQueryProcessor _movieInfoQueryProcessor;
        private readonly HttpClient _omdbClient;

        public MovieSearchService(IMovieInfoQueryProcessor movieInfoQueryProcessor)
        {
            _movieInfoQueryProcessor = movieInfoQueryProcessor;
            _omdbClient = new HttpClient();
        }

        public async Task<MovieInfoModel> SearchByTitle(string title, bool lookForDbFirst)
        {
            if (lookForDbFirst)
            {
                // Search in database.
                MovieInfo movieInfo = await Task.Run(() => 
                    _movieInfoQueryProcessor.GetAllMovieInfos().FirstOrDefault(info => info.Title == title));

                if (movieInfo != null)
                {
                    return new MovieInfoModel
                    {
                        Title = movieInfo.Title,
                        ImdbId = movieInfo.ImdbId,
                        MovieInfoJson = movieInfo.MovieInfoJson
                    };
                }
            }

            // Search in omdb api.
            var requestUri = new Uri(OmdbApiEndPointUri, $"?apikey={OmdbApiAuthKey}&t={title}");

            string responseMessage = await _omdbClient.GetStringAsync(requestUri);

            JObject json = JObject.Parse(responseMessage);

            if (json["Response"].Value<string>() == "False")
            {
                throw new NotFoundException("Searched movie was not found in the omdb api!");
            }

            var movieInfoModel = new MovieInfoModel
            {
                ImdbId = json["imdbID"].Value<string>(),
                Title = title,
                MovieInfoJson = responseMessage
            };

            return movieInfoModel;
        }

        public async Task<MovieInfoModel> SearchByImdbId(string imdbId, bool lookForDbFirst)
        {
            if (lookForDbFirst)
            {
                // Search in database.
                MovieInfo movieInfo = await Task.Run(() => 
                    _movieInfoQueryProcessor.GetAllMovieInfos().FirstOrDefault(info => info.ImdbId == imdbId));

                if (movieInfo != null)
                {
                    return new MovieInfoModel
                    {
                        Title = movieInfo.Title,
                        ImdbId = movieInfo.ImdbId,
                        MovieInfoJson = movieInfo.MovieInfoJson
                    };
                }
            }

            // Search in omdb api.
            var requestUri = new Uri(OmdbApiEndPointUri, $"?apikey={OmdbApiAuthKey}&i={imdbId}");

            string responseMessage = await _omdbClient.GetStringAsync(requestUri);

            JObject json = JObject.Parse(responseMessage);

            if (json["Response"].Value<string>() == "False")
            {
                throw new NotFoundException("Searched movie was not found in the omdb api!");
            }

            var movieInfoModel = new MovieInfoModel
            {
                ImdbId = imdbId,
                Title = json["Title"].Value<string>(),
                MovieInfoJson = responseMessage
            };

            return movieInfoModel;
        }
    }
}
