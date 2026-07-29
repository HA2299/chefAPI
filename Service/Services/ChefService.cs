using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interfaces;
using Repository.Entities;
using Repository.interfaces;
using Repository.Repositories;
using AutoMapper;
using Service.Dto;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace Service.Services
{
    public class ChefService : IService<ChefDto>, IChef
    {
        private readonly IRepository<Chef> _repository;
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IMapper _mapper;
        private readonly MyRecipesMapper _myRecipesMapper; // 1. הוסף את המשתנה כאן
        public ChefService(IRepository<Chef> repository, IRepository<Recipe> recipeRepository,IMapper mapper, MyRecipesMapper myRecipesMapper)
        {
            _repository = repository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _myRecipesMapper = myRecipesMapper;
        }

        public async Task<ChefDto> AddItemAsync(ChefDto item)
        {
            var chef = _mapper.Map<ChefDto, Chef>(item);
            var addedRecipe = await _repository.AddItemAsync(chef);
            return _mapper.Map<Chef,ChefDto>(addedRecipe);
        }

        public async Task DeleteItemAsync(int id)
        {
           await _repository.DeleteItemAsync(id);
        }

        public async Task<List<ChefDto>> GetAllAsync()
        {
            var chefs = await _repository.GetAllAsync();
            return _mapper.Map<List<Chef>, List<ChefDto>>(chefs);
        }

        public async Task<ChefDto> GetByIdAsync(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            return _mapper.Map<Chef, ChefDto>(chef);
        }
        public async Task UpdateItemAsync(int id, ChefDto item)
        {
            var chef = _mapper.Map<ChefDto, Chef>(item);
            await _repository.UpdateItemAsync(id, chef);
        }
        //public async Task<List<RecipeDto>> GetRecipesByChefIdAsync(int chefId)
        //{
        //    var recipes = await _recipeRepository.GetAllAsync();
        //    var filteredRecipes = recipes.Where(r => r.ChefId == chefId).ToList();

        //    return _mapper.Map<List<RecipeDto>>(filteredRecipes);
        //}
public async Task<List<RecipeDto>> GetRecipesByChefIdAsync(int chefId)
{
            var recipes = await _recipeRepository.GetAllAsync();
            var filteredRecipes = recipes.Where(r => r.ChefId == chefId).ToList();
            // הדפסה ראשונית לקונסול שמתחילה את התהליך
            Console.WriteLine($"Started processing {recipes.Count} recipes for Chef ID: {chefId}");

    var tasks = filteredRecipes.Select(async r => {
        var dto = _mapper.Map<RecipeDto>(r);
        
        // הדפסה לכל מתכון בנפרד כדי לראות אם הוא מעובד
        Console.WriteLine($"Processing image for recipe: {r.ImageUrl}");
        
        dto.Image = await _myRecipesMapper.fromStringToByteAsync(r.ImageUrl); 
        
        // בדיקה אם התמונה חזרה ריקה
        if (dto.Image == null) {
            Console.WriteLine($"Warning: Failed to load image for {r.ImageUrl}");
        }
        
        return dto;
    });

    var recipeDtos = await Task.WhenAll(tasks);
    
    Console.WriteLine("Finished processing all recipes.");
    return recipeDtos.ToList();
}

        public async Task<ChefDto> GetByUserIdAsync(int userId)
        {
            var chefs = await _repository.GetAllAsync();
            var chef = chefs.FirstOrDefault(c => c.UserId == userId);
            return _mapper.Map<ChefDto>(chef);
        }

        public async Task UpdateChefRatingAsync(int chefId, double newRating)
        {
            var recipes = await GetRecipesByChefIdAsync(chefId); 
            if (recipes.Count == 0)
            {
                return;
            }
            double averageRating = Math.Round(recipes.Average(r => r.Rating), 3); 
            var chef = await GetByIdAsync(chefId);
            if (chef != null)
            {
                chef.AverageRating = averageRating;
                await UpdateItemAsync(chefId, chef);
            }
        }
     }

}
