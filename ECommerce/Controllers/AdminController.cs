using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var result = await _service.AdLogin(email,password);

                return Ok(new
                {
                    message = result
                });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Invalid Credentials"
                });
            }
        }
    }
}
