using Newtonsoft.Json;

namespace PetNetwork.Application.Utility;
public class StringDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.String)
        {
            throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
        }

        var stringValue = (string)reader.Value!;
        if (!DateTime.TryParse(stringValue, out var result))
        {
            throw new JsonSerializationException($"Invalid date format: {stringValue}");
        }

        return result;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}

