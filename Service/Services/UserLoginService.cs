using System;
using System.Collections.Generic;
using System.Linq;
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

        public User Authenticate(UserLogin user)
        {
            return _repository.GetAll().FirstOrDefault(x => x.Email == user.Email && x.Name == user.UserName);
        }

        public User GetUserByToken(string token)
        {
            var userId = ValidateToken(token); // שיטה שתאמת את הטוקן ותחזיר את מזהה המשתמש

            if (userId == null)
            {
                return null; // טוקן לא חוקי
            }

            // קבל את המשתמש על סמך מזהה המשתמש
            var user = _repository.GetById(userId.Value); // הנחה: userId הוא int?
            return user;
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await Task.FromResult(GetUserByToken(token)); // קריאה לשיטה הסינכרונית
        }

        // שיטה לדוגמה לאימות טוקן
        private int? ValidateToken(string token)
        {
            // כאן תוכל להוסיף לוגיקה לאימות הטוקן
            // החזר את מזהה המשתמש אם הטוקן תקף, אחרת החזר null
            return 1; // החזר את מזהה המשתמש המתאים (כמו 1 לדוגמה)
        }

        public bool Register(UserRegister userRegister)
        {
            // לוגיקת רישום משתמשים
            var existingUser = userRepository.GetByEmail(userRegister.Email);
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

            _repository.AddItem(newUser);
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
