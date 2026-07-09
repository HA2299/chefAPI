using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.GenAI;
using Google.GenAI.Types;

[ApiController]
[Route("[controller]")]
public class RecipeAIController : ControllerBase
{
    private readonly IRecipeAIService _aiService;


    public RecipeAIController(IRecipeAIService aiService)
    {
        _aiService = aiService;
    }


    [HttpPost("ask")]
    public async Task<IActionResult> AskGemma(
        [FromBody] QuestionRequest request)
    {
        var result = await _aiService.GenerateRecipe(request.Question);

        return Ok(result);
    }
}

public class QuestionRequest
{
    public string Question { get; set; }
}
