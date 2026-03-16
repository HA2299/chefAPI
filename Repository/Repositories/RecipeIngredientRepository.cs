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
    public class RecipeIngredientRepository(IContext context) : IRepository<RecipeIngredient>
    {

        private readonly IContext _context = context;

        public async Task<RecipeIngredient> AddItemAsync(RecipeIngredient item)
        {
            await _context.RecipeIngredients.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.RecipeIngredients.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<RecipeIngredient>> GetAllAsync()
        {
            return await _context.RecipeIngredients.Include(x => x.Ingredient).ToListAsync();
        }

        public async Task<RecipeIngredient> GetByIdAsync(int id)
        {
            return await _context.RecipeIngredients.SingleAsync(x => x.Id == id);
        }

        public async Task UpdateItemAsync(int id, RecipeIngredient item)
        {
            var RecipeIngredient =  await GetByIdAsync(id);
            RecipeIngredient.Id = item.Id;
            RecipeIngredient.RecipeId = item.RecipeId;
            RecipeIngredient.Recipe = item.Recipe;
            RecipeIngredient.IngredientId = item.IngredientId;
            RecipeIngredient.Ingredient= item.Ingredient;
            RecipeIngredient.Quantity = item.Quantity;
            RecipeIngredient.Unit = item.Unit;
            await _context.Save();
        }
    }
}
