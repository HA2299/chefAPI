using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;

namespace WebApiProjectChef.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogin login;

        public RegisterController(ILogin login)
        {
            this.login = login;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            var result = login.Register(userRegister);
            if (result)
            {
                return Ok("User registered successfully.");
            }
            return BadRequest("User registration failed.");
        }
    }
}
