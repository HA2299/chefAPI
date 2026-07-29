using Microsoft.Extensions.Configuration;
using Google.GenAI;

public class RecipeAIService : IRecipeAI
{
    private readonly string _apiKey;

    public RecipeAIService(IConfiguration configuration)
    {
        _apiKey = configuration["GoogleApi:ApiKey"];
    }


    public async Task<string> GenerateRecipe(string question)
    {
        var client = new Client(apiKey: _apiKey);

        var response = await client.Models.GenerateContentAsync(
            model: "gemma-3-4b-it",
            contents: question
        );

        return response.Text;
    }
}