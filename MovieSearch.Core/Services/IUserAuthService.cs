using System.Threading.Tasks;

namespace MovieSearch.Core.Services
{
    public interface IUserAuthService
    {
        Task<string> AuthenticateUser(string username, string password);

        bool ValidateJwtToken(string jwtTokenString);
    }
}