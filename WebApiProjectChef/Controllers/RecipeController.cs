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
        public List<RecipeDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public RecipeDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<RecipeController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public RecipeDto Post([FromForm] RecipeDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] RecipeDto value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }

        [HttpGet("count")]
        public int GetRecipeCount()
        {
            return service.GetAll().Count;
        }

        [HttpGet("search")]
        public ActionResult<List<RecipeDto>> Search([FromQuery] string query)
        {
            var recipes = service.GetAll();
            if (!string.IsNullOrWhiteSpace(query))
            {
                recipes = recipes.Where(r =>
                    (r.Title != null && r.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    ((chefService.GetById(r.ChefId).User.Name).Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (recipeIngredientService.GetByRecipeId(r.Id).Select(i => i.Ingredient.Name).Any(name => name.Contains(query, StringComparison.OrdinalIgnoreCase)))
                ).ToList();
            }

            return Ok(recipes);
        }

        [HttpPost("rate")]
        public IActionResult RateRecipe([FromBody] RatingData ratingData)
        {
            if (ratingData == null)
            {
                return BadRequest("Rating data cannot be null");
            }

            var recipe = service.GetById(int.Parse(ratingData.recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            recipeService.AddRating(recipe.Id, ratingData.ratingValue);

            var chef = chefService.GetById(recipe.ChefId);
            if (chef != null)
            {
                _chef.UpdateChefRating(chef.Id, recipe.Rating);
            }
            return Ok(new { success = true });
        }

        // GET api/Recipe/rate/{recipeId}
        [HttpGet("rate/{recipeId}")]
        public IActionResult GetUserRating(string recipeId)
        {
            var recipe = service.GetById(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            return Ok(new { rating = recipe.Rating });
        }

        // GET api/recipe/ratings/{recipeId}
        [HttpGet("ratings/{recipeId}")]
        public IActionResult GetRecipeRatings(string recipeId)
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
            var recipe = service.GetById(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }
            return Ok(new { rating = recipe.Rating });
        }

        // GET api/recipe/all-ratings/{recipeId}
        [HttpGet("all-ratings/{recipeId}")]
        public IActionResult GetAllRatingsForRecipe(string recipeId)
        {
            var recipe = service.GetById(int.Parse(recipeId));
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            return Ok(new { allRatings = recipe.Rating });
        }
    }
}