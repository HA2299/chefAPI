using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.GenAI;
using Google.GenAI.Types;

[ApiController]
[Route("[controller]")]
public class RecipeAIController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RecipeAIController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleApi:ApiKey"]; // קריאה מהמפתח בקובץ הקונפיגורציה
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskGemma([FromBody] QuestionRequest request)
    {
        try
        {
            var geminiClient = new Client(apiKey: _apiKey);
            var geminiResponse = await geminiClient.Models.GenerateContentAsync(
                model: "gemma-3-4b-it", contents: request.Question);

            return Ok(geminiResponse);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"שגיאה בבקשה ל-Gemini API: {ex.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"שגיאה כללית: {e.Message}");
        }
    }
}

public class QuestionRequest
{
    public string Question { get; set; }
}
