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
    public class RecipeRepository(IContext context) : IRepository<Recipe>
    {

        private readonly IContext _context = context;

        public async Task<Recipe> AddItemAsync(Recipe item)
        {
            await _context.Recipes.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task DeleteItemAsync(int id)
        {
            _context.Recipes.Remove(await GetByIdAsync(id));
            await _context.Save();
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _context.Recipes.ToListAsync(); 
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await _context.Recipes.SingleAsync();
        }

        public async Task UpdateItemAsync(int id, Recipe item)
        {
            var Recipe =  await GetByIdAsync(id);
            Recipe.Id = item.Id;
            Recipe.Title = item.Title;
            Recipe.Description = item.Description;
            Recipe.Ingredients = item.Ingredients;
            Recipe.Instructions = item.Instructions;
            Recipe.PreparationTime = item.PreparationTime;
            Recipe.CookingTime = item.CookingTime;
            Recipe.DifficultyLevel = item.DifficultyLevel;
            Recipe.ChefId = item.ChefId;
            Recipe.Chef= item.Chef;
            Recipe.CategoryId = item.CategoryId;
            Recipe.Category = item.Category;
            Recipe.NumDoses=item.NumDoses;
            Recipe.Rating=item.Rating;
            Recipe.RatingCount=item.RatingCount;
            await _context.Save();
        }
    }
}
