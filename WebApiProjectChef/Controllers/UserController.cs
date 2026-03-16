using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IService<User> service) : ControllerBase
    {
        private readonly IService<User> service = service;

        // GET: api/<UserController>
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<User> Post([FromBody] User value)
        {
            return await service.AddItemAsync(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }

        [HttpGet("count")]
        public async Task<int> GetUserCount()
        {
            var users = await service.GetAllAsync();
            return users.Count; 
        }
    }
}
