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

        public RecipeDto AddItem(RecipeDto item)
        {
            var existingCategory = categoryRepository.GetById(item.CategoryId);
            if (existingCategory == null)
            {
                if (Enum.IsDefined(typeof(CategoryType), item.CategoryId))
                {
                    var categoryType = (CategoryType)item.CategoryId;
                    var newCategory = new Category { Name = categoryType };
                    categoryRepository.AddItem(newCategory);
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
                var existingIngredient = ingredientRepository.GetByName(ingredient.Name);

                if (existingIngredient == null)
                {
                    ingredientRepository.AddItem(ingredient);
                }
                else
                {
                    ingredient.Id = existingIngredient.Id;
                }
            }

            var recipe = mapper.Map<RecipeDto, Recipe>(item);
            var addedRecipe = recipeRepository.AddItem(recipe);
            return mapper.Map<Recipe, RecipeDto>(addedRecipe);
        }

        public void DeleteItem(int id)
        {
            recipeRepository.DeleteItem(id);
        }

        public List<RecipeDto> GetAll()
        {
            var recipes = recipeRepository.GetAll();
            return mapper.Map<List<Recipe>, List<RecipeDto>>(recipes);
        }

        public RecipeDto GetById(int id)
        {
            var recipe = recipeRepository.GetById(id);
            return mapper.Map<Recipe, RecipeDto>(recipe);
        }
        public void UpdateItem(int id, RecipeDto item)
        {
            var recipe = mapper.Map<RecipeDto, Recipe>(item);
            recipeRepository.UpdateItem(id, recipe);
        }

        public void AddRating(int recipeId, int ratingValue)
        {
            var recipe = recipeRepository.GetById(recipeId);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found.");
            }
            double totalRating = recipe.Rating * recipe.RatingCount + ratingValue;
            recipe.RatingCount++;
            recipe.Rating = totalRating / recipe.RatingCount;
            recipeRepository.UpdateItem(recipeId, recipe);
        }

        public double GetRecipeRating(int recipeId)
        {
            var recipe = recipeRepository.GetById(recipeId);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found.");
            }

            return recipe.Rating;
        }
    }
}
