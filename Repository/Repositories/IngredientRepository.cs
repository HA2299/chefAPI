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

        public Ingredient AddItem(Ingredient item)
        {
            _context.Ingredients.Add(item);

            _context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Ingredients.Remove(GetById(id));
            _context.Save();
        }

        public List<Ingredient> GetAll()
        {
            return _context.Ingredients.ToList();
        }

        public Ingredient GetById(int id)
        {
            return _context.Ingredients.ToList().FirstOrDefault(x => x.Id == id);
        }

        public Ingredient GetByName(string name)
        {
            return _context.Ingredients.FirstOrDefault(i => i.Name == name);
        }

        public void UpdateItem(int id, Ingredient item)
        {
            var Ingredient = GetById(id);
            Ingredient.Id = item.Id;
            Ingredient.Name = item.Name;
            //Ingredient.Recipes = item.Recipes;
            _context.Save();
        }
    }
}
