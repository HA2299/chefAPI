using System;
using Newtonsoft.Json;
using Repository.Entities;

namespace Common
{
    public class CategoryTypeConverter : JsonConverter<CategoryType>
    {
        public override CategoryType ReadJson(JsonReader reader, Type objectType, CategoryType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string value = (string)reader.Value;
            return (CategoryType)Enum.Parse(typeof(CategoryType), value, true); // true for case-insensitive
        }

        public override void WriteJson(JsonWriter writer, CategoryType value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}