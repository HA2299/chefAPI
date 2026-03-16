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
       
        public async Task<RecipeIngredient> AddItemAsync(RecipeIngredient ingredient)
        {
            if (!Enum.IsDefined(typeof(UnitType), ingredient.Unit))
            {
                throw new ArgumentException("Invalid unit type.");
            }

            return await repository.AddItemAsync(ingredient);
        }


        public async Task DeleteItemAsync(int id)
        {
            await repository.DeleteItemAsync(id);
        }

        public async Task<List<RecipeIngredient>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<RecipeIngredient> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
        {
            var recipeIngredients= await repository.GetAllAsync();
            return recipeIngredients.Where(ri => ri.RecipeId == recipeId).ToList();
        }

        public async Task UpdateItemAsync(int id, RecipeIngredient item)
        {
            await repository.UpdateItemAsync(id, item);
        }

    }
}
