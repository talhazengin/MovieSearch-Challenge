﻿using System.Threading.Tasks;
using MovieSearch.Data.Models.Movie;

namespace MovieSearch.Services
{
    public interface IOmdbMovieSearchService
    {
        Task<MovieInfoModel> SearchByTitle(string title, bool lookForDbFirst);

        Task<MovieInfoModel> SearchByImdbId(string imdbId, bool lookForDbFirst);
    }
}