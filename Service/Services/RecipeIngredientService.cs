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

        public async Task<bool> UpdateIngredientByRecipeIdAsync(int recipeId, RecipeIngredient ingredient)
        {
            var existingIngredients = await GetByRecipeIdAsync(recipeId);
            if (existingIngredients == null || !existingIngredients.Any())
            {
                return false;
            }

            var existingIngredient = existingIngredients.FirstOrDefault(ri => ri.Id == ingredient.Id);
            if (existingIngredient != null)
            {
                // עדכון פרטי המרכיב הקיים
                existingIngredient.IngredientId = ingredient.IngredientId;
                existingIngredient.Quantity = ingredient.Quantity;
                existingIngredient.Unit = ingredient.Unit;
                await repository.UpdateItemAsync(existingIngredient.Id, existingIngredient);
            }
            else
            {
                // הוספת מרכיב חדש אם לא קיים
                ingredient.RecipeId = recipeId; // ודא שה-RecipeId מוגדר
                RecipeIngredient recipeIngredient = new RecipeIngredient
                {
                   Quantity = ingredient.Quantity,
                   Unit = ingredient.Unit,
                   RecipeId=recipeId,
                    IngredientId = ingredient.Id
                }; 

                   
                await repository.AddItemAsync(recipeIngredient);
            }

            return true;
        }

        public async Task UpdateItemAsync(int id, RecipeIngredient item)
        {
            await repository.UpdateItemAsync(id, item);
        }

    }
}
