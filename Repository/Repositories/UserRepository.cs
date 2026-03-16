using Microsoft.EntityFrameworkCore;
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

        public async Task<User> AddItemAsync(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Users.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.SingleAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleAsync(x=>x.Email == email);
        }

        public async Task UpdateItemAsync(int id, User item)
        {
            var user = await GetByIdAsync(id);
            user.Id = item.Id;
            user.Name = item.Name;
            user.Email = item.Email;
            user.Password = item.Password;
            user.Role = item.Role;
            await _context.Save();
        }
    }
}
