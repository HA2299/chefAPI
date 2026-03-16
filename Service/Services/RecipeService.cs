using System;
using System.Collections.Generic;
using Service.Interfaces;
using Repository.Entities;
using Repository.Repositories;
using Repository.interfaces;
using Service.Dto;
using AutoMapper;

namespace Service.Services
{
    public class RecipeService : IService<RecipeDto>, IRecipe
    {
        private readonly IRepository<Recipe> recipeRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Ingredient> ingredientRepository;
        private readonly IMapper mapper;

        public RecipeService(IRepository<Recipe> recipeRepository, IRepository<Category> categoryRepository, IRepository<Ingredient> ingredientRepository, IMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.categoryRepository = categoryRepository;
            this.ingredientRepository = ingredientRepository;
            this.mapper = mapper;
        }

        public async Task<RecipeDto> AddItemAsync(RecipeDto item)
        {
            var existingCategory = await categoryRepository.GetByIdAsync(item.CategoryId);
            if (existingCategory == null)
            {
                if (Enum.IsDefined(typeof(CategoryType), item.CategoryId))
                {
                    var categoryType = (CategoryType)item.CategoryId;
                    var newCategory = new Category { Name = categoryType };
                    await categoryRepository.AddItemAsync(newCategory);
                    item.CategoryId = newCategory.Id;
                }
                else
                {
                    throw new ArgumentException("Invalid category ID.");
                }
            }

            if (!Enum.IsDefined(typeof(DifficultyLevel), item.DifficultyLevel))
            {
                throw new ArgumentException("Invalid difficulty level.");
            }

            foreach (var ingredient in item.Ingredients)
            {
                var existingIngredient = await ingredientRepository.GetByNameAsync(ingredient.Name);

                if (existingIngredient == null)
                {
                    await ingredientRepository.AddItemAsync(ingredient);
                }
                else
                {
                    ingredient.Id = existingIngredient.Id;
                }
            }

            var recipe = mapper.Map<RecipeDto, Recipe>(item);
            var addedRecipe = await recipeRepository.AddItemAsync(recipe);
            return mapper.Map<Recipe, RecipeDto>(addedRecipe);
        }

        public async Task DeleteItemAsync(int id)
        {
            await recipeRepository.DeleteItemAsync(id);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            var recipes = await recipeRepository.GetAllAsync();
            return mapper.Map<List<Recipe>, List<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> GetByIdAsync(int id)
        {
            var recipe = await recipeRepository.GetByIdAsync(id);
            return mapper.Map<Recipe, RecipeDto>(recipe);
        }
        public async Task UpdateItemAsync(int id, RecipeDto item)
        {
            var recipe = mapper.Map<RecipeDto, Recipe>(item);
            await recipeRepository.UpdateItemAsync(id, recipe);
        }

        public async Task AddRatingAsync(int recipeId, int ratingValue)
        {
            var recipe = await recipeRepository.GetByIdAsync(recipeId);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found.");
            }
            double totalRating = Math.Round(recipe.Rating * recipe.RatingCount + ratingValue, 3);
            recipe.RatingCount++;
            recipe.Rating = totalRating / recipe.RatingCount;
            await recipeRepository.UpdateItemAsync(recipeId, recipe);
        }

        public async Task<double> GetRecipeRatingAsync(int recipeId)
        {
            var recipe = await recipeRepository.GetByIdAsync(recipeId);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found.");
            }

            return recipe.Rating;
        }
    }
}
