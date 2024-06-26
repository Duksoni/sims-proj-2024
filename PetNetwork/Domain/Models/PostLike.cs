using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;
public class PostLike : ISerializable
{
    public string Id { get; set; }
    public string Author { get; set; }
    public Post Post { get; set; }
    public bool Deleted { get; set; }

    public PostLike(string id, string author, Post post)
    {
        Id = id;
        Author = author;
        Post = post;
        Deleted = false;
    }
}

