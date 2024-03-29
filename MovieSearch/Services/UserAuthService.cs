﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieSearch.Core.Services;
using MovieSearch.Data.Models.User;
using MovieSearch.Data.QueryProcessors;

namespace MovieSearch.Services
{
    public class UserAuthService : IUserAuthService
    {
        private const string JwtKeyString = "t*a-l=h+a-z?e*n^g#i'n";

        private readonly byte[] _jwtKeyByte = Encoding.ASCII.GetBytes(JwtKeyString);

        private readonly IUserQueryProcessor _userQueryProcessor;
        private readonly ILogger<UserAuthService> _logger;

        public UserAuthService(IUserQueryProcessor userQueryProcessor, ILogger<UserAuthService> logger)
        {
            _userQueryProcessor = userQueryProcessor;
            _logger = logger;
        }

        public Task<string> AuthenticateUser(string username, string password)
        {
            return Task.Run(() =>
            {
                User user = _userQueryProcessor.GetAllUsers().FirstOrDefault(u => u.Username == username && u.Password == password);

                // return null if user not found.
                if (user == null)
                {
                    return null;
                }

                // authentication successful so generate and return jwt token.
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtKeyByte), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            });
        }

        public bool ValidateJwtToken(string jwtTokenString)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(_jwtKeyByte),
            };

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(jwtTokenString, tokenValidationParameters, out SecurityToken jwtToken);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Got an error while validating Jwt Token!");
                return false;
            }
        }
    }
}
