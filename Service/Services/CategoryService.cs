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
    public class CategoryService(IRepository<Category> repository) : IService<Category>
    {
        private readonly IRepository<Category> repository = repository;

        public Task<Category> AddItemAsync(Category item)
        {

            return repository.AddItemAsync(item);
        }

        public Task DeleteItemAsync(int id)
        {
            return repository.DeleteItemAsync(id);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
           return repository.GetByIdAsync(id);
        }

        public Task UpdateItemAsync(int id, Category item)
        {
           return repository.UpdateItemAsync(id, item);
        }
    }
}
