using Blog.Services.Api.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsers();
            return Ok(user);
        }

        [HttpGet("{userid:guid}")]
        public async Task<IActionResult> GetUsersById(Guid userid)
        {
            var user = await _userService.GetUserById(userid);
            return Ok(user);
        }
    }
}
