using CodeFirst.Models;
using Google;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<List<ChefDto>> Get()
        {
            return await service.GetAllAsync();
        }

        // GET api/<ChefController>/5
        [HttpGet("{id}")]
        public async Task<ChefDto> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<ChefDto>> Post([FromForm] ChefDto value)
        {
            // בדיקה אם נשלח קובץ בכלל
            if (value.FileImage != null && value.FileImage.Length > 0)
            {
                var directoryPath = Path.Combine(Environment.CurrentDirectory, "images_chefs");
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                var path = Path.Combine(directoryPath, value.FileImage.FileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    await value.FileImage.CopyToAsync(fs);
                }
            }

            var result = await service.AddItemAsync(value);
            return Ok(result);
        }

        // PUT api/<ChefController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ChefDto value)
        {
            await service.UpdateItemAsync(id, value);
        }

        // DELETE api/<ChefController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItemAsync(id);
        }

        [HttpGet("count")]
        public async Task<int> GetChefCount()
        {
            var chefs = await service.GetAllAsync();
            return chefs.Count; 
        }

        [HttpGet("{chefId}/recipes")]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipesByChefId(int chefId)
        {
            var recipes = await chef.GetRecipesByChefIdAsync(chefId);
            return Ok(recipes ?? new List<RecipeDto>());
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ChefDto>> GetChefByUserId(int userId)
        {
            var chef = await _chef.GetByUserIdAsync(userId);
            return chef;
        }


    }
}
