using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRecipeIngredient
    {
        Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId);
        Task<bool> UpdateIngredientByRecipeIdAsync(int recipeId, RecipeIngredient ingredient);
    }
}
