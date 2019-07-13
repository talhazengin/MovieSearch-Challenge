using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.Data.Models.User;
using MovieSearch.Services;

namespace MovieSearch.Controllers
{
    [AllowAnonymous]
    [Route("moviesearch/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserAuthService _userAuthService;

        public UserController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            User user = _userAuthService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [HttpGet("authenticate")]
        public IActionResult GetAUser()
        {
            var user = new User { Username = "MyUser" };

            return Ok(user);
        }
    }
}