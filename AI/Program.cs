using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Embeddings;
using AI.VectorStore;
using AI.Indexing; 
using AI.Retrieval;
using Microsoft.Extensions.Configuration;
using AI.Documents;
using Repository.Repositories;
using CodeFirst.Models;
using AI.LLM;
using Service.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var embeddingService = new GeminiEmbeddingService(configuration);
var vectorStore = new QdrantVectorStore();
await vectorStore.InitializeAsync();
Console.WriteLine("Qdrant initialized");

// 2. אתחול אובייקטים
var dbContext = new ChefDB();
var recipeRepository = new RecipeRepository(dbContext);
var recipeIngredientRepository =
    new RecipeIngredientRepository(dbContext);

var recipeIngredientService =
    new RecipeIngredientService(recipeIngredientRepository);
var recipeDocumentBuilder =
    new RecipeDocumentBuilder(recipeIngredientService);
var indexer = new RecipeIndexer(recipeRepository, recipeDocumentBuilder, embeddingService, vectorStore);
var retriever = new RecipeRetriever(embeddingService, vectorStore);

Console.WriteLine("Indexing recipes...");
await indexer.IndexAllRecipesAsync();
Console.WriteLine("Indexing completed.");

string userQuery = "something with tomamtoes and eggs";
var docs = await retriever.GetRelevantRecipesAsync(userQuery);
Console.WriteLine($"Found {docs.Count} relevant recipes.");


var llmService = new OpenRouterLLMService(configuration);
try
{
    var response = await llmService.GenerateResponseAsync(
        userQuery,
        docs);
    Console.WriteLine(response);
}
catch (Exception ex)
{
    Console.WriteLine($"Error generating response: {ex.Message}");
}