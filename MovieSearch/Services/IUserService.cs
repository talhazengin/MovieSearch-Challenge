using MovieSearch.Data.Models;

namespace MovieSearch.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}