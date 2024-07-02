using PetNetwork.Domain.Enums;

namespace PetNetwork.Domain.Models;

public class PetHealth
{
    public string Description { get; set; }

    public PetHealthCondition Condition { get; set; }


    public PetHealth()
    {
        Description = string.Empty;
        Condition = PetHealthCondition.Healthy;
    }

    public PetHealth(string description, PetHealthCondition condition)
    {
        Description = description;
        Condition = condition;
    }
}