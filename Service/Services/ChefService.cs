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

        public ChefService(IRepository<Chef> repository, IRepository<Recipe> recipeRepository,IMapper mapper)
        {
            _repository = repository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public ChefDto AddItem(ChefDto item)
        {
            var chef = _mapper.Map<ChefDto, Chef>(item);
            var addedRecipe = _repository.AddItem(chef);
            return _mapper.Map<Chef, ChefDto>(addedRecipe);
        }

        public void DeleteItem(int id)
        {
            _repository.DeleteItem(id);
        }

        public List<ChefDto> GetAll()
        {
            var chefs = _repository.GetAll();
            return _mapper.Map<List<Chef>, List<ChefDto>>(chefs);
        }

        public ChefDto GetById(int id)
        {
            var chef = _repository.GetById(id);
            return _mapper.Map<Chef, ChefDto>(chef);
        }
        public void UpdateItem(int id, ChefDto item)
        {
            var chef = _mapper.Map<ChefDto, Chef>(item);
            _repository.UpdateItem(id, chef);
        }
        public List<RecipeDto> GetRecipesByChefId(int chefId)
        {
            var recipes = _recipeRepository.GetAll()
                .Where(r => r.ChefId == chefId)
                .ToList();

            return _mapper.Map<List<RecipeDto>>(recipes);
        }

        public ChefDto GetByUserId(int userId)
        {
            var chef = _repository.GetAll().FirstOrDefault(c => c.UserId == userId);
            return _mapper.Map<ChefDto>(chef);
        }

        public void UpdateChefRating(int chefId, double newRating)
        {
            var recipes = GetRecipesByChefId(chefId); 
            if (recipes.Count == 0)
            {
                return;
            }
            double averageRating = recipes.Average(r => r.Rating);
            var chef = GetById(chefId);
            if (chef != null)
            {
                Console.WriteLine(chef.AverageRating);
                chef.AverageRating = averageRating;
                Console.WriteLine(chef.Id);
                Console.WriteLine(chef.AverageRating);
                UpdateItem(chefId, chef);
                Console.WriteLine(GetById(chefId).AverageRating);

            }
        }
     }

}
