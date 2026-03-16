using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;

namespace WebApiProjectChef.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogin login;
        private readonly IConfiguration config;

        public AuthController(ILogin login, IConfiguration configuration)
        {
            this.login = login;
            this.config = configuration;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var result = await login.Register(userRegister);
            if (result)
            {
                return Ok("User registered successfully.");
            }
            return BadRequest("User registration failed.");
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<UserWithToken> Login([FromBody] UserLogin user)
        {
            var user1 = await login.Authenticate(user);
            if (user1 != null)
            {
                var token = GenerateToken(user1);
                return new UserWithToken
                {
                    User = user1,
                    Token = token
                };
            }
            throw new Exception("User not found...");
        }

        // GET api/auth/getUserByToken
        [HttpGet("getUserByToken")]
        public async Task<User> GetUserByToken([FromHeader] string token)
        {
            var user = await login.GetUserByToken(token);
            if (user != null)
            {
                return user;
            }
            throw new Exception("Invalid token");
        }
        private string GenerateToken(User user1)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user1.Name),
                new Claim(ClaimTypes.Email, user1.Email),
                new Claim(ClaimTypes.Role, user1.Role)
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class UserWithToken
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
