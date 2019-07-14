using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieSearch.Core.Exceptions;
using MovieSearch.Data.Models.Movie;
using MovieSearch.Data.QueryProcessors;
using Newtonsoft.Json.Linq;

namespace MovieSearch.Services
{
    public class OmdbMovieSearchService : IOmdbMovieSearchService
    {
        private const string OmdbApiAuthKey = "fa904133";

        private static readonly Uri OmdbApiEndPointUri = new Uri("http://www.omdbapi.com/", UriKind.Absolute);

        private readonly IMovieInfoQueryProcessor _movieInfoQueryProcessor;
        private readonly HttpClient _omdbClient;

        public OmdbMovieSearchService(IMovieInfoQueryProcessor movieInfoQueryProcessor)
        {
            _movieInfoQueryProcessor = movieInfoQueryProcessor;
            _omdbClient = new HttpClient();
        }

        public async Task<MovieInfoModel> SearchByTitle(string title)
        {
            var requestUri = new Uri(OmdbApiEndPointUri, $"?apikey={OmdbApiAuthKey}&t={title}");

            string responseMessage = await _omdbClient.GetStringAsync(requestUri);

            JObject json = JObject.Parse(responseMessage);

            if (json["Response"].Value<string>() == "False")
            {
                throw new NotFoundException("Searched movie was not found in the omdb api!");
            }

            var movieInfoModel = new MovieInfoModel
            {
                ImdbId = json["ImdbId"].Value<string>(),
                Title = title,
                MovieInfoJson = responseMessage
            };

            // Add found movie info to the database.
            await _movieInfoQueryProcessor.CreateMovieInfo(movieInfoModel);

            return movieInfoModel;
        }

        public async Task<MovieInfoModel> SearchByImdbId(string imdbId)
        {
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

            // Add found movie info to the database.
            await _movieInfoQueryProcessor.CreateMovieInfo(movieInfoModel);

            return movieInfoModel;
        }
    }
}
