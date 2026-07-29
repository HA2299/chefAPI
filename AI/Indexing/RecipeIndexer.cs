using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Interfaces;
using AI.Models;
using Repository.Entities;
using Repository.interfaces;

namespace AI.Indexing
{
    public class RecipeIndexer
    {
        private readonly IRepository<Recipe> _recipeRepository;

        private readonly IRecipeDocumentBuilder _documentBuilder;

        private readonly IEmbeddingService _embeddingService;

        private readonly IVectorStore _vectorStore;


        public RecipeIndexer(
    IRepository<Recipe> recipeRepository,
    IRecipeDocumentBuilder documentBuilder,
    IEmbeddingService embeddingService,
    IVectorStore vectorStore)
        {
            _recipeRepository = recipeRepository;
            _documentBuilder = documentBuilder;
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
        }

        public async Task IndexAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllAsync();
            Console.WriteLine($"Found {recipes.Count()} recipes in database.");

            foreach (var recipe in recipes)
            {
                var doc = await _documentBuilder.BuildAsync(recipe);
                var vector = await _embeddingService.CreateEmbeddingAsync(doc.Content);
                var vectorDoc = new VectorDocument
                {
                    Id = recipe.Id.ToString(),
                    Vector = vector,
                    Metadata = doc.Metadata
                };
                vectorDoc.Metadata["Content"] = doc.Content;

                await _vectorStore.StoreAsync(vectorDoc);
            }
        }
    }
}
