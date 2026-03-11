using Repository.Entities;
using Repository.interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>, IUser
    {
        private readonly IContext _context;

        public UserRepository(IContext context)
        {
            _context = context;
        }

        public User AddItem(User item)
        {
            _context.Users.Add(item);
            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Users.Remove(GetById(id));
            _context.Save();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.ToList().FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.ToList().FirstOrDefault(x => x.Email == email);
        }

        public void UpdateItem(int id, User item)
        {
            var user = GetById(id);
            user.Id = item.Id;
            user.Name = item.Name;
            user.Email = item.Email;
            user.Password = item.Password;
            user.Role = item.Role;
            _context.Save();
        }
    }
}
