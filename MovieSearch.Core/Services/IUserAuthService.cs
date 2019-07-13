namespace MovieSearch.Core.Services
{
    public interface IUserAuthService
    {
        string AuthenticateUser(string username, string password);

        bool ValidateJwtToken(string jwtTokenString);
    }
}