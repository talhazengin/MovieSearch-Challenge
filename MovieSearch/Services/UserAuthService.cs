using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieSearch.Data.Models;
using MovieSearch.Data.Models.User;
using MovieSearch.Queries;

namespace MovieSearch.Services
{
    public class UserAuthService : IUserAuthService
    {
        private const string JWTSecretKey = "t*a-l=h+a";

        private readonly IUserQueryProcessor _userQueryProcessor;

        public UserAuthService(IUserQueryProcessor userQueryProcessor)
        {
            _userQueryProcessor = userQueryProcessor;
        }

        public User Authenticate(string username, string password)
        {
            User user = _userQueryProcessor.GetAllUsers().FirstOrDefault(u => u.Username == username && u.Password == password);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(JWTSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        //public IEnumerable<User> GetAll()
        //{
        //// return users without passwords
        //return _users.Select(x => {
        //x.Password = null;
        //return x;
        //});
        //}
    }
}
