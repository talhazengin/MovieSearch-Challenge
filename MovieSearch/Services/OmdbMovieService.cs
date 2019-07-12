using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using MovieSearch.Data.Models;

namespace MovieSearch.Services
{
    public class OmdbMovieService : IOmdbMovieService
    {
        private const string OmdbApiAuthKey = "fa904133";

        private static readonly Uri OmdbApiEndPointUri = new Uri("www.omdbapi.com");

        private readonly HttpClient _omdbClient;

        public OmdbMovieService()
        {
            _omdbClient = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("apikey", OmdbApiAuthKey)
                }
            };
        }

        public async Task<Movie> SearchByTitle(string title)
        {
            HttpResponseMessage httpResponseMessage = await _omdbClient.GetAsync(OmdbApiEndPointUri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string movieInfoJson = await httpResponseMessage.Content.ReadAsStringAsync();

                return new Movie();
            }
            else
            {
                return null;
            }
        }
    }
}
