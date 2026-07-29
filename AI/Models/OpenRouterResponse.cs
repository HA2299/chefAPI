using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace AI.Models;

public class OpenRouterResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = new();
}

public class Choice
{
    [JsonPropertyName("message")]
    public ResponseMessage Message { get; set; } = new();
}

public class ResponseMessage
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}