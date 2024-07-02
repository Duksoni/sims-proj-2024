using Newtonsoft.Json;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class AnimalType : ISerializable
{

    [JsonProperty("Type")]
    public string Id { get; set; }

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

    public AnimalType()
    {
        Id = string.Empty;
    }

    public AnimalType(string type)
    {
        Id = type;
    }

    [JsonConstructor]
    public AnimalType(string id, bool deleted)
    {
        Id = id;
        _deleted = deleted;
    }

    public override string ToString() => Id;
}