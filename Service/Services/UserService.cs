using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interfaces;
using Repository.Entities;
using Repository.interfaces;

namespace Service.Services
{
    public class UserService(IRepository<User> repository) : IService<User>
    {
        private readonly IRepository<User> repository = repository;

        public Task<User> AddItemAsync(User item)
        {
            return repository.AddItemAsync(item);
        }

        public Task DeleteItemAsync(int id)
        {
            return repository.DeleteItemAsync(id);
        }

        public Task<List<User>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return repository.GetByIdAsync(id);
        }

        public Task UpdateItemAsync(int id, User item)
        {
            return repository.UpdateItemAsync(id, item);
        }
    }
}
