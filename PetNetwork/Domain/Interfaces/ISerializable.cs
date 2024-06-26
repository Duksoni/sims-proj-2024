namespace PetNetwork.Domain.Interfaces;

public interface ISerializable
{
    string Id { get; }
    bool Deleted { get; set; }
}