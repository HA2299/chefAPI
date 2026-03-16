using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController(IService<Ingredient> service) : ControllerBase
    {
        private readonly IService<Ingredient> service = service;

        // GET: api/<IngredientController>
        [HttpGet]
        public async Task<List<Ingredient>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public async Task<Ingredient> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        // POST api/<IngredientController>
        [HttpPost]
        public async Task<Ingredient> Post([FromBody] Ingredient value)
        {
            return await service.AddItemAsync(value);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Ingredient value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }
    }
}
