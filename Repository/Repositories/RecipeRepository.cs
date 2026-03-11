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

        public Recipe AddItem(Recipe item)
        {
            _context.Recipes.Add(item);
            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Recipes.Remove(GetById(id));
            _context.Save();
        }

        public List<Recipe> GetAll()
        {
            return _context.Recipes.ToList(); 
        }

        public Recipe GetById(int id)
        {
            return _context.Recipes.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Recipe item)
        {
            var Recipe = GetById(id);
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
            _context.Save();
        }
    }
}
