using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class MessageGroup : ISerializable
{
    public string Id { get; set; } // group name
    public IList<string> Members { get; set; }
    public bool Deleted {  get; set; }

    public MessageGroup(string id, IList<string> members)
    {
        Id = id;
        Members = members;
        Deleted = false;
    }
}
