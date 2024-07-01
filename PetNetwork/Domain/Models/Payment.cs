using Newtonsoft.Json;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Payment : ISerializable
{
    public string Id { get; }

    public string Payer { get; set; }

    public string Purpose { get; set; }

    public decimal Amount { get; set; }

    [JsonConverter(typeof(StringDateTimeConverter))]
    public DateTime PaymentDate { get; set; }
    

    private bool _deleted;
    public bool Deleted
    {
        get => _deleted;
        set
        {
            if (!_deleted && value)
                _deleted = value;
        }
    }

    public Payment(string payer, string purpose, decimal amount, DateTime paymentDate)
    {
        Id = IdGenerator.Generate();
        Payer = payer;
        Purpose = purpose;
        Amount = amount;
        PaymentDate = paymentDate;
    }

    [JsonConstructor]
    public Payment(string id, string payer, string purpose, decimal amount, DateTime paymentDate, bool deleted)
    {
        Id = id;
        Payer = payer;
        Purpose = purpose;
        Amount = amount;
        PaymentDate = paymentDate;
        _deleted = deleted;
    }
}