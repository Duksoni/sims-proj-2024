namespace PetNetwork.Domain.Models;

public class AnimalType
{
    public string Type { get; set; }

    public AnimalType()
    {
        Type = string.Empty;
    }

    public AnimalType(string type)
    {
        Type = type;
    }
}