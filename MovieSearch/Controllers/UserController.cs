using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.Core.Services;
using MovieSearch.Data.Models.User;
using MovieSearch.Data.QueryProcessors;
using MovieSearch.Filters;
using MovieSearch.Services;

namespace MovieSearch.Controllers
{
    [AllowAnonymous]
    [Route("moviesearch/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IUserQueryProcessor _userQueryProcessor;

        public UserController(IUserAuthService userAuthService, IUserQueryProcessor userQueryProcessor)
        {
            _userAuthService = userAuthService;
            _userQueryProcessor = userQueryProcessor;
        }

        [HttpPost("create")]
        [ValidateModel]
        public async Task CreateUser([FromBody]UserModel userModel)
        {
            await _userQueryProcessor.CreateUser(userModel);
        }

        [HttpPost("authenticate")]
        [ValidateModel]
        public IActionResult AuthenticateUser([FromBody]UserModel userModel)
        {
            string token = _userAuthService.AuthenticateUser(userModel.Username, userModel.Password);

            if (token == null)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }

            return Ok(token);
        }
    }
}