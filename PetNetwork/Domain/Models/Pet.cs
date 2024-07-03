using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Pet : ISerializable
{
    public string Id { get; }

    public string Breed { get; set; }

    public string Colour { get; set; }

    public int BirthYear { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PetOwnership Ownership { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PetAdoptionStatus Status { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Gender Gender { get; set; }

    public Address Location { get; set; }

    public string FinalOwner { get; set; }

    public AnimalType Animal {  get; set; }

    public PetHealth Health { get; set; }

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

    public Pet()
    {
        Id = string.Empty;
        Breed = string.Empty;
        Colour = string.Empty;
        BirthYear = -1;
        Gender = Gender.Male;
        Location = new Address(); // null?
        FinalOwner = string.Empty;
        Animal = new AnimalType();
        Health = new PetHealth();
    }

    public Pet(string breed, string colour, int birthYear, Gender gender, Address location, AnimalType animal, PetHealth health)
    {
        Id = IdGenerator.Generate();
        Breed = breed;
        Colour = colour;
        BirthYear = birthYear;
        Gender = gender;
        Location = location;
        FinalOwner = string.Empty;
        Animal = animal;
        Health = health;
    }

    [JsonConstructor]
    public Pet(string id, string breed, string colour, int birthYear, PetOwnership ownership, PetAdoptionStatus status,
        Gender gender, Address location, string finalOwner, AnimalType animal, PetHealth health, bool deleted)
    {
        Id = id;
        Breed = breed;
        Colour = colour;
        BirthYear = birthYear;
        Ownership = ownership;
        Status = status;
        Gender = gender;
        Location = location;
        FinalOwner = finalOwner;
        Animal = animal;
        Health = health;
        _deleted = deleted;
    }

    public override string ToString() => $"{Gender} {Animal} {Breed} ({Colour}) Born in {BirthYear} {Health}";
}