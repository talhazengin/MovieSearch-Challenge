using System;

namespace MovieSearch.Data.Models.Movie
{
    [Serializable]
    public class MovieInfoModel
    {
        public string Title { get; set; }

        public string ImdbId { get; set; }

        public string MovieInfoJson { get; set; }
    }
}
