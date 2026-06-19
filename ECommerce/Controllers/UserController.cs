using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _service.CreateUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _service.Login(loginUser.Email, loginUser.Password);

            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Invalid Credentials"
                });
            }

            return Ok(new
            {
                message = "Successfully logged in",
                userId = user.Id,
                userName = user.Name
            });
        }
    }
}
