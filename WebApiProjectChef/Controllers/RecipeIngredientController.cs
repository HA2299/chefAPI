using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController(IService<RecipeIngredient> service,IRecipeIngredient recipeIngredientService) : ControllerBase
    {
        private readonly IService<RecipeIngredient> service = service;
        private readonly IRecipeIngredient recipeIngredientService=recipeIngredientService;

        // GET: api/<RecipeIngredientController>
        [HttpGet]
        public async Task<List<RecipeIngredient>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<RecipeIngredientController>/5
        [HttpGet("{id}")]
        public async Task<RecipeIngredient> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpGet("recipeId/{recipeId}")]
        public async Task<List<RecipeIngredient>> GetByRecipeId(int recipeId)
        {
            return await recipeIngredientService.GetByRecipeIdAsync(recipeId); 
        }


        // POST api/<RecipeIngredientController>
        [HttpPost]
        public async Task<RecipeIngredient> Post([FromBody] RecipeIngredient value)
        {
            return await service.AddItemAsync(value);
        }

        // PUT api/<RecipeIngredientController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] RecipeIngredient value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<RecipeIngredientController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }
    }
}
