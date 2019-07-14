using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.Core.Services;
using MovieSearch.Data.Models.User;
using MovieSearch.Data.QueryProcessors;
using MovieSearch.Filters;

namespace MovieSearch.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IUserQueryProcessor _userQueryProcessor;

        public UserController(IUserAuthService userAuthService, IUserQueryProcessor userQueryProcessor)
        {
            _userAuthService = userAuthService;
            _userQueryProcessor = userQueryProcessor;
        }

        [ValidateModel]
        [HttpPost("Create")]
        public async Task CreateUser([FromBody]UserModel userModel)
        {
            await _userQueryProcessor.CreateUser(userModel);
        }

        [ValidateModel]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody]UserModel userModel)
        {
            string token = await _userAuthService.AuthenticateUser(userModel.Username, userModel.Password);

            if (token == null)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }

            return Ok(token);
        }
    }
}