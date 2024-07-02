using Newtonsoft.Json;
using PetNetwork.Application.Utility.Constants;
using PetNetwork.Domain.Models;
using System.IO;
using System.Text.Json;

namespace PetNetwork.Application.Utility;

public class PaymentExtractor
{
    private readonly string _filePath;

    public PaymentExtractor(string filePath)
    {
        if (!File.Exists(filePath)) 
            throw new ArgumentException("File not found on the provided path");
        _filePath = filePath;
    }

    public IList<PaymentDTO> TryExtracting(out string error)
    {
        var payments = new List<PaymentDTO>();
        error = string.Empty;
        try
        {
            var enumeratedEntries = GetPaymentEntries();
            payments.AddRange(enumeratedEntries.Select(DeserializeEntry));
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        return payments;
    }

    private JsonElement.ArrayEnumerator GetPaymentEntries()
    {
        using StreamReader reader = new(_filePath);
        var fileContents = reader.ReadToEnd();
        var jsonDocument = JsonDocument.Parse(fileContents);
        return jsonDocument.RootElement.GetProperty(GlobalConstants.BankStatementPropertyToExtract).EnumerateArray();
    }

    private static PaymentDTO DeserializeEntry(JsonElement entry) => 
        JsonConvert.DeserializeObject<PaymentDTO>(entry.GetRawText())
        ?? throw new JsonReaderException("Failed to deserialize an entry");
}