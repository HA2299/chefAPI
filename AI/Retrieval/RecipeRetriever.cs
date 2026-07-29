using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Interfaces;
using AI.Models;

namespace AI.Retrieval;

public class RecipeRetriever
{
    private readonly IEmbeddingService _embeddingService;
    private readonly IVectorStore _vectorStore;

    public RecipeRetriever(IEmbeddingService embeddingService, IVectorStore vectorStore)
    {
        _embeddingService = embeddingService;
        _vectorStore = vectorStore;
    }

    public async Task<List<VectorDocument>> GetRelevantRecipesAsync(string userQuery)
    {
        var queryVector = await _embeddingService.CreateEmbeddingAsync(userQuery);
        return await _vectorStore.SearchAsync(queryVector);
    }
}
