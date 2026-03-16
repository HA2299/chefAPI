using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController(IService<Favorite> service) : ControllerBase
    {
        private readonly IService<Favorite> service = service;

        // GET: api/<FavoriteController>
        [HttpGet]
        public async Task<List<Favorite>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<FavoriteController>/5
        [HttpGet("{id}")]
        public async Task<Favorite> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        // POST api/<FavoriteController>
        [HttpPost]
        public async Task<Favorite> Post([FromBody] Favorite value)
        {
            return await service.AddItemAsync(value);
        }

        // PUT api/<FavoriteController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Favorite value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<FavoriteController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }
    }
}
