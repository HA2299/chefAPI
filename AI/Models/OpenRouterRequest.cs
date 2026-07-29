using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AI.Models;

public class OpenRouterRequest
{
    public string Model { get; set; } = string.Empty;

    public List<Message> Messages { get; set; } = new();
}

public class Message
{
    public string Role { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}