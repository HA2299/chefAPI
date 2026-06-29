using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.interfaces;
using Repository.Repositories;
using Service.Dto;
using Service.Interfaces;

namespace Service.Services
{
    public class UserLoginService : ILogin
    {
        private readonly IRepository<User> _repository;
        private readonly IUser userRepository;

        public UserLoginService(IRepository<User> _repository, IUser userRepository)
        {
            this._repository = _repository;
            this.userRepository = userRepository;
        }

        public async Task<User> Authenticate(UserLogin user)
        {
            var users =await _repository.GetAllAsync();
            return users.FirstOrDefault(x => x.Email == user.Email && x.Name == user.UserName);
        }

        public async Task<User> GetUserByToken(string token)
        {
            var userId = ValidateToken(token);

            if (userId == null)
            {
                return null;
            }
            var user = await _repository.GetByIdAsync(userId.Value);
            return user;
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await Task.FromResult(await GetUserByToken(token));
        }

        private int? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                return null; // טוקן לא תקף
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return null; // לא נמצא Claim של מזהה המשתמש
            }
            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null; // לא ניתן להמיר את המזהה
        }


        public async Task<bool> Register(UserRegister userRegister)
        {
            // לוגיקת רישום משתמשים
            var existingUser = await userRepository.GetByEmailAsync(userRegister.Email);
            if (existingUser != null)
            {
                return false;
            }

            var newUser = new User
            {
                Name = userRegister.Name,
                Email = userRegister.Email,
                Password = HashPassword(userRegister.Password),
                Role = userRegister.Role,
            };

            await _repository.AddItemAsync(newUser);
            return true;
        }

        private string HashPassword(string password)
        {
            // יצירת Salt
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;

                // חישוב ה-hash
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = hmac.ComputeHash(passwordBytes);

                // חיבור ה-Salt וה-hash
                var hashWithSalt = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, hashWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, hashWithSalt, salt.Length, hashBytes.Length);

                // החזרת ה-hash המוצפן כ-string ב-base64
                return Convert.ToBase64String(hashWithSalt);
            }
        }
    }
}
