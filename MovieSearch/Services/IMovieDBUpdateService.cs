using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSearch.Services
{
    public interface IMovieDbUpdateService
    {
        void StartBackgroundUpdateProcess();
    }
}
