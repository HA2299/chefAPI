using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category> service;
        public CategoryController(IService<Category> service)
        {
            this.service = service;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<Category> Post([FromBody] Category value)
        {
            return await service.AddItemAsync(value);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Category value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }
    }
}
