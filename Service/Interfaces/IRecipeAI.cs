public interface IRecipeAI
{
    Task<string> GenerateRecipe(string question);
}