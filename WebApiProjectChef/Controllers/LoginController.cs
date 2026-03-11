//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Repository.Entities;
//using Service.Dto;
//using Service.Interfaces;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApiProjectChef.Controllers
//{
//    [Route("api/auth/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly ILogin login;
//        private readonly IConfiguration config;
//        public LoginController(ILogin login, IConfiguration configuration)
//        {
//            this.login = login;
//            this.config = configuration;
//        }
//        // GET: api/<LoginController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<LoginController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }


//        public class UserWithToken
//        {
//            public User User { get; set; }
//            public string Token { get; set; }
//        }


//        // POST api/<LoginController>
//        [HttpPost]

//        public UserWithToken Post([FromBody] UserLogin user)
//        {
//            var user1 = login.Authenticate(user);
//            if (user1 != null)
//            {
//                var token = GenerateToken(user1);
//                return new UserWithToken
//                {
//                    User = user1,
//                    Token = token
//                }; // מחזיר אובייקט עם המשתמש והטוקן
//            }
//            throw new Exception("user not found..."); // או החזר null
//        }

//        //יצירת טוקן
//        private string GenerateToken(User user1)
//        {
//            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
//            //אלגוריתם להצפנה
//            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
//            var claims = new[] {
//            new Claim(ClaimTypes.Name,user1.Name),
//            new Claim(ClaimTypes.Email,user1.Email),
//           new Claim(ClaimTypes.Role,user1.Role)
//            //new Claim("Id",user1.Id.ToString()),
//            //new Claim(ClaimTypes.GivenName,user1.Name)
//            };
//            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
//                claims,
//                expires: DateTime.Now.AddMinutes(15),
//                signingCredentials: credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }



//        // PUT api/<LoginController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<LoginController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }

//    }
//}
