using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(IService<RecipeDto> service,IService<ChefDto> chefService,
        IRecipeIngredient recipeIngredientService,IRecipe recipeService,IChef chef) : ControllerBase
    {
        private readonly IService<RecipeDto> service = service;
        private readonly IService<ChefDto> chefService = chefService;
        private readonly IRecipeIngredient recipeIngredientService= recipeIngredientService;
        private readonly IRecipe recipeService = recipeService;
        private readonly IChef _chef=chef;

        // GET: api/<RecipeController>
        [HttpGet]
        public async Task<List<RecipeDto>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public async Task<RecipeDto> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        // POST api/<RecipeController>
        [HttpPost]
        public async Task<RecipeDto> Post([FromForm] RecipeDto value)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "images_recipes", value.FileImage.FileName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                value.FileImage.CopyTo(fs);
                fs.Close();
            }
            return await service.AddItemAsync(value);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromForm] RecipeDto value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }

        [HttpGet("count")]
        public async Task<int> GetRecipeCount()
        {
            var recipes = await service.GetAllAsync();
            return recipes.Count;
        }

        [HttpGet("averageRate")]
        public async Task<double> GetAverageRating()
        {
            var recipes = await service.GetAllAsync();
            if (recipes == null || !recipes.Any())
            {
                return 0;
            }

            double averageRating = recipes.Average(r => r.Rating);
            return Math.Round(averageRating, 3);
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<RecipeDto>>> Search([FromQuery] string query)
        {
            var recipes = await service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var filteredRecipes = new List<RecipeDto>();

                foreach (var r in recipes)
                {
                    bool matchesQuery =
                        (r.Title != null && r.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                        ((await chefService.GetByIdAsync(r.ChefId)).User.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                        (await recipeIngredientService.GetByRecipeIdAsync(r.Id)).Any(i => i.Ingredient.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

                    if (matchesQuery)
                    {
                        filteredRecipes.Add(r);
                    }
                }

                recipes = filteredRecipes;
            }

            return Ok(recipes);
        }


        [HttpPost("rate")]
        public async Task<IActionResult> RateRecipe([FromBody] RatingData ratingData)
        {
            if (ratingData == null)
            {
                return BadRequest("Rating data cannot be null");
            }

            var recipe = await service.GetByIdAsync(int.Parse(ratingData.recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            await recipeService.AddRatingAsync(recipe.Id, ratingData.ratingValue);

            var chef = await chefService.GetByIdAsync(recipe.ChefId);
            if (chef != null)
            {
                await _chef.UpdateChefRatingAsync(chef.Id, recipe.Rating);
            }
            return Ok(new { success = true });
        }

        // GET api/Recipe/rate/{recipeId}
        [HttpGet("rate/{recipeId}")]
        public async Task<IActionResult> GetUserRating(string recipeId)
        {
            var recipe = await service.GetByIdAsync(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            return Ok(new { rating = recipe.Rating });
        }

        // GET api/recipe/ratings/{recipeId}
        [HttpGet("ratings/{recipeId}")]
        public async Task<IActionResult> GetRecipeRatings(string recipeId)
        {

            if (string.IsNullOrEmpty(recipeId) || recipeId == "undefined")
            {
                return BadRequest("Invalid recipe ID");
            }

            int id;
            if (!int.TryParse(recipeId, out id))
            {
                return BadRequest("Invalid recipe ID format");
            }
            var recipe = await service.GetByIdAsync(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }
            return Ok(new { rating = recipe.Rating });
        }

        // GET api/recipe/all-ratings/{recipeId}
        [HttpGet("all-ratings/{recipeId}")]
        public async Task<IActionResult> GetAllRatingsForRecipe(string recipeId)
        {
            var recipe = await service.GetByIdAsync(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            return Ok(new { allRatings = recipe.Rating });
        }
    }
}