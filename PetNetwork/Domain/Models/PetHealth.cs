using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Domain.Enums;

namespace PetNetwork.Domain.Models;

public class PetHealth
{
    public string Description { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PetHealthCondition Condition { get; set; }


    public PetHealth()
    {
        Description = string.Empty;
    }

    public PetHealth(string description, PetHealthCondition condition = PetHealthCondition.Healthy)
    {
        Description = description;
        Condition = condition;
    }

    public override string ToString() => $"Condition: {Condition} ({Description})";
}