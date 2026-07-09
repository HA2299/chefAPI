using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.GenAI;
using Google.GenAI.Types;

namespace WebApiProjectChef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeAIController : ControllerBase
    {
        private readonly IRecipeAI _aiService;


        public RecipeAIController(IRecipeAI aiService)
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
}