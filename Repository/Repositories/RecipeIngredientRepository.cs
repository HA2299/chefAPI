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

        public RecipeIngredient AddItem(RecipeIngredient item)
        {
            _context.RecipeIngredients.Add(item);

            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.RecipeIngredients.Remove(GetById(id));
            _context.Save();
        }

        public List<RecipeIngredient> GetAll()
        {
            return (List<RecipeIngredient>)_context.RecipeIngredients.Include(x => x.Ingredient).ToList();
        }

        public RecipeIngredient GetById(int id)
        {
            return _context.RecipeIngredients.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, RecipeIngredient item)
        {
            var RecipeIngredient = GetById(id);
            RecipeIngredient.Id = item.Id;
            RecipeIngredient.RecipeId = item.RecipeId;
            RecipeIngredient.Recipe = item.Recipe;
            RecipeIngredient.IngredientId = item.IngredientId;
            RecipeIngredient.Ingredient= item.Ingredient;
            RecipeIngredient.Quantity = item.Quantity;
            RecipeIngredient.Unit = item.Unit;
            _context.Save();
        }
    }
}
