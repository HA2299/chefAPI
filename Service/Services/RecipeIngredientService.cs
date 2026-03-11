using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interfaces;
using Repository.Entities;
using Repository.interfaces;
using Repository.Repositories;

namespace Service.Services
{
    public class RecipeIngredientService(IRepository<RecipeIngredient> repository) : IService<RecipeIngredient>, IRecipeIngredient
    {
        private readonly IRepository<RecipeIngredient> repository = repository;
       
        public RecipeIngredient AddItem(RecipeIngredient ingredient)
        {
            Console.WriteLine(ingredient.Unit);
            if (!Enum.IsDefined(typeof(UnitType), ingredient.Unit))
            {
                throw new ArgumentException("Invalid unit type.");
            }

            return repository.AddItem(ingredient);
        }


        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<RecipeIngredient> GetAll()
        {
            return repository.GetAll();
        }

        public RecipeIngredient GetById(int id)
        {
            return repository.GetById(id);
        }

        public List<RecipeIngredient> GetByRecipeId(int recipeId)
        {
            return repository.GetAll().Where(ri => ri.RecipeId == recipeId).ToList();
        }

        public void UpdateItem(int id, RecipeIngredient item)
        {
            repository.UpdateItem(id, item);
        }

    }
}
