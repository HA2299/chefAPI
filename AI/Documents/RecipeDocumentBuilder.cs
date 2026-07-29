using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Interfaces;
using Repository.Entities;
using Repository.interfaces;
using Service.Interfaces;

namespace AI.Documents
{
    public class RecipeDocumentBuilder : IRecipeDocumentBuilder
    {
        private readonly IRecipeIngredient recipeIngredientService;

        public RecipeDocumentBuilder(IRecipeIngredient recipeIngredientService)
        {
            this.recipeIngredientService = recipeIngredientService;
        }
        public async Task<RecipeDocument> BuildAsync(Recipe recipe)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Recipe Name: {recipe.Title}");
            sb.AppendLine($"Description: {recipe.Description}");
            if (recipe.Category != null)
            {
                sb.AppendLine($"Category: {recipe.Category.Name}");
            }
            sb.AppendLine($"Difficulty: {recipe.DifficultyLevel}");
            sb.AppendLine($"Preparation Time: {recipe.PreparationTime} minutes");
            sb.AppendLine($"Cooking Time: {recipe.CookingTime} minutes");
            sb.AppendLine($"Servings: {recipe.NumDoses}");
            var recipeIngredients =
                await recipeIngredientService.GetByRecipeIdAsync(recipe.Id); recipeIngredients = recipeIngredients.Where(ri => ri.RecipeId == recipe.Id).ToList();
            sb.AppendLine("Ingredients:");

            foreach (var ingredient in recipeIngredients)
            {
                sb.AppendLine(
                    $"- {ingredient.Ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }
            sb.AppendLine();
            sb.AppendLine("Instructions:");
            sb.AppendLine(recipe.Instructions);
            var document = new RecipeDocument();
            document.RecipeId = recipe.Id;
            document.Content = sb.ToString();
            document.Metadata.Add("Difficulty", recipe.DifficultyLevel.ToString());

            document.Metadata.Add("PreparationTime", recipe.PreparationTime);

            document.Metadata.Add("CookingTime", recipe.CookingTime);

            document.Metadata.Add("Servings", recipe.NumDoses);

            document.Metadata.Add("Rating", recipe.Rating);
            if (recipe.Category != null)
            {
                document.Metadata.Add("Category", recipe.Category.Name);
            }
            return document;
        }
    }
}
