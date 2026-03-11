using System;
using Newtonsoft.Json;

namespace Repository.Entities
{
    public class CategoryTypeConverter : JsonConverter<CategoryType>
    {
        public override CategoryType ReadJson(JsonReader reader, Type objectType, CategoryType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = (string)reader.Value;
                if (Enum.TryParse(typeof(CategoryType), value, true, out var result))
                {
                    return (CategoryType)result;
                }
                throw new JsonSerializationException($"Invalid value '{value}' for CategoryType.");
            }
            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing CategoryType.");
        }

        public override void WriteJson(JsonWriter writer, CategoryType value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}