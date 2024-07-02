using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.IO;

namespace PetNetwork.Application.Utility;

public static class JsonSchemaValidator
{

    public static bool IsValidDocument(string schemaFilePath, string filePath, out string validationErrors)
    {
        try
        {
            var schemaJson = File.ReadAllText(schemaFilePath);
            var schema = JSchema.Parse(schemaJson);

            var jsonContent = File.ReadAllText(filePath);
            var jsonObject = JObject.Parse(jsonContent);

            var isValid = jsonObject.IsValid(schema, out IList<string> errors);

            validationErrors = string.Join(Environment.NewLine, errors);

            return isValid;
        }
        catch (Exception ex)
        {
            validationErrors = $"Error validating the document: {ex.Message}";
            return false;
        }
    }
}