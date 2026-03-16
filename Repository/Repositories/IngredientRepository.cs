using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class IngredientRepository(IContext context) : IRepository<Ingredient>
    {

        private readonly IContext _context = context;

        public async Task<Ingredient> AddItemAsync(Ingredient item)
        {
            await _context.Ingredients.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Ingredients.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return  await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await _context.Ingredients.SingleAsync(x => x.Id == id);
        }

        public async Task<Ingredient> GetByNameAsync(string name)
        {
            return await _context.Ingredients.SingleAsync(x=>x.Name == name);
        }

        public async Task UpdateItemAsync(int id, Ingredient item)
        {
            var Ingredient = await GetByIdAsync(id);
            Ingredient.Id = item.Id;
            Ingredient.Name = item.Name;
            //Ingredient.Recipes = item.Recipes;
            await _context.Save();
        }
    }
}
