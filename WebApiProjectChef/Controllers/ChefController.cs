using CodeFirst.Models;
using Google;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjectChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController(IService<ChefDto> service,IChef chef) : ControllerBase
    {
        private readonly IService<ChefDto> service = service;
        private readonly IChef _chef=chef;

        // GET: api/<ChefController>
        [HttpGet]
        public List<ChefDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ChefController>/5
        [HttpGet("{id}")]
        public ChefDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ChefController>
        [HttpPost]
        public ChefDto Post([FromBody] ChefDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<ChefController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ChefDto value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<ChefController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }

        [HttpGet("count")]
        public int GetChefCount()
        {
            return service.GetAll().Count; 
        }

        [HttpGet("{chefId}/recipes")]
        public ActionResult<List<RecipeDto>> GetRecipesByChefId(int chefId)
        {
            var recipes = chef.GetRecipesByChefId(chefId);
            return Ok(recipes ?? new List<RecipeDto>());
        }


        //[HttpPost]
        //[Route("{chefId}/rate")]
        //public async Task<IActionResult> UpdateChefAverageRating(int chefId, [FromBody] decimal newRating)
        //{
        //    using (ChefDB context = new ChefDB())
        //    {
        //        var chef = await context.Chefs.FindAsync(chefId);
        //        if (chef == null)
        //        {
        //            return NotFound("Chef not found");
        //        }
        //        chef.AverageRating = (chef.AverageRating + newRating) / 2;

        //        await context.SaveChangesAsync();
        //        return Ok();
        //    }
        //}

        [HttpGet("user/{userId}")]
        public ActionResult<ChefDto> GetChefByUserId(int userId)
        {
            var chef = _chef.GetByUserId(userId);
            return chef;
        }


    }
}
