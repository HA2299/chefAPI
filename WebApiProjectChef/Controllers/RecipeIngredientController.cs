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
        public List<RecipeIngredient> Get()
        {
            return service.GetAll();
        }

        // GET api/<RecipeIngredientController>/5
        [HttpGet("{id}")]
        public RecipeIngredient Get(int id)
        {
            return service.GetById(id);
        }

        [HttpGet("recipeId/{recipeId}")]
        public List<RecipeIngredient> GetByRecipeId(int recipeId)
        {
            return recipeIngredientService.GetByRecipeId(recipeId); // ודא שהשירות שלך תומך בשיטה זו
        }


        // POST api/<RecipeIngredientController>
        [HttpPost]
        public RecipeIngredient Post([FromBody] RecipeIngredient value)
        {
            Console.WriteLine(value);
            return service.AddItem(value);
        }

        // PUT api/<RecipeIngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] RecipeIngredient value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<RecipeIngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
