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
    public class IngredientService(IRepository<Ingredient> repository) : IService<Ingredient>
    {
        private readonly IRepository<Ingredient> repository = repository;

        public Task<Ingredient> AddItemAsync(Ingredient item)
        {

            return repository.AddItemAsync(item);
        }

        public Task DeleteItemAsync(int id)
        {
            return repository.DeleteItemAsync(id);
        }

        public Task<List<Ingredient>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<Ingredient> GetByIdAsync(int id)
        {
            return repository.GetByIdAsync(id);
        }
        
        public Task<Ingredient> GetByNameAsync(string name)
        {
            return repository.GetByNameAsync(name);
        }

        public Task UpdateItemAsync(int id, Ingredient item)
        {
            return repository.UpdateItemAsync(id, item);
        }
    }
}
