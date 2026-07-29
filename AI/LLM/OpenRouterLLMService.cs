using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AI.Interfaces;
using AI.Models;
using Microsoft.Extensions.Configuration;

namespace AI.LLM;

public class OpenRouterLLMService
{
    private readonly HttpClient _httpClient;
    private readonly string _model;

    public OpenRouterLLMService(IConfiguration configuration)
    {
        var apiKey = configuration["OpenRouter:ApiKey"]
            ?? throw new InvalidOperationException("OpenRouter API key is missing.");

        _model = configuration["OpenRouter:Model"]
            ?? throw new InvalidOperationException("OpenRouter model is missing.");

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://openrouter.ai/api/v1/")
        };

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost");
        _httpClient.DefaultRequestHeaders.Add("X-Title", "Recipe RAG");
    }

    public async Task<string> GenerateResponseAsync(
        string userQuery,
        List<VectorDocument> contextDocs)
    {
        if (contextDocs.Count == 0)
        {
            return "I couldn't find any relevant recipes.";
        }

        var context = string.Join(
            "\n\n----------------------\n\n",
            contextDocs.Select(d => d.Metadata["Content"]?.ToString()));

        var request = new OpenRouterRequest
        {
            Model = _model,
            Messages =
            [
                new Message
                {
                    Role = "system",
                    Content =
"""
You are an expert chef.

Answer ONLY using the information provided in the context.

Rules:
- Never invent ingredients.
- Never invent cooking steps.
- If the answer is not in the context, clearly say that you don't have enough information.
- Give concise and helpful answers.
"""
                },

                new Message
                {
                    Role = "user",
                    Content =
$"""
Context:

{context}

-------------------------

User Question:

{userQuery}
"""
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(
            "chat/completions",
            request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"OpenRouter Error ({(int)response.StatusCode}): {error}");
        }

        var result =
            await response.Content.ReadFromJsonAsync<OpenRouterResponse>();

        return result?.Choices.FirstOrDefault()?.Message.Content
               ?? "The model returned an empty response.";
    }
}