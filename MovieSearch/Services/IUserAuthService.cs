using MovieSearch.Data.Models.User;

namespace MovieSearch.Services
{
    public interface IUserAuthService
    {
        User Authenticate(string username, string password);
    }
}