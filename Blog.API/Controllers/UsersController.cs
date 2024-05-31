using Blog.Common.Models.User;
using Blog.Services.Api;
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
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

        [HttpGet("{userId:guid}")]
        //[Authorize]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{AddUsers}")]
        public async Task<IActionResult> AddUsers([FromBody]CreateUserModel model)
        {
            try
            {
                var user = await _userService.AddUser(model);
                return Ok(user);    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{Login}")]
        public async Task<IActionResult> Login([FromBody]LoginUserModel model)
        {
            try
            {
                var user = await _userService.Login(model);
                return Ok(user);    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{usersid:guid}")]
        public async Task<IActionResult> UpdataUser(Guid userId, [FromBody] UpdateUserModel model)
        {
            try
            {
                var user = await _userService.UpdateUser(userId, model);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{Users:guid}")]

        public async Task<IActionResult> DeleteUser(Guid userid)
        {
            try
            {
                var user = await _userService.DeleteUser(userid);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
