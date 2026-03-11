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
        public List<Ingredient> Get()
        {
            return service.GetAll();
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id}")]
        public Ingredient Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<IngredientController>
        [HttpPost]
        public Ingredient Post([FromBody] Ingredient value)
        {
            return service.AddItem(value);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Ingredient value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
