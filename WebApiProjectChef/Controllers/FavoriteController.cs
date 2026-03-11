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
        public List<Favorite> Get()
        {
            return service.GetAll();
        }

        // GET api/<FavoriteController>/5
        [HttpGet("{id}")]
        public Favorite Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<FavoriteController>
        [HttpPost]
        public Favorite Post([FromBody] Favorite value)
        {
            return service.AddItem(value);
        }

        // PUT api/<FavoriteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Favorite value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<FavoriteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
