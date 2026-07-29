using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AI.Interfaces;
using Google.GenAI;
using Microsoft.Extensions.Configuration;
using Google.GenAI.Types;

namespace AI.Embeddings;

public class GeminiEmbeddingService : IEmbeddingService
{
    private readonly Client _client;

    public GeminiEmbeddingService(IConfiguration configuration)
    {
        var apiKey = configuration["GoogleApi:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("Google API Key was not found.");

        _client = new Client(apiKey: apiKey);
    }

    public async Task<float[]> CreateEmbeddingAsync(string text)
    {
        var response = await _client.Models.EmbedContentAsync(
model: "gemini-embedding-001",
contents: text
        );

        return response.Embeddings[0].Values
            .Select(v => (float)v)
            .ToArray();
    }
}