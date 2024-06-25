using Newtonsoft.Json;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class PetSociety : ISerializable
{
    public string Id { get; }
    
    public string Name { get; set; }

    public string BankAccount { get; set; }

    
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

    public PetSociety(string name, string bankAccount)
    {
        Id = IdGenerator.Generate();
        Name = name;
        BankAccount = bankAccount;
    }

    public PetSociety(string id, string name, string bankAccount)
    {
        Id = id;
        Name = name;
        BankAccount = bankAccount;
    }

    [JsonConstructor]
    public PetSociety(string id, string name, string bankAccount, bool deleted)
    {
        Id = id;
        Name = name;
        BankAccount = bankAccount;
        _deleted = deleted;
    }
}
