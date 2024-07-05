using PetNetwork.Domain.Enums;

namespace PetNetwork.Domain.Models;

public class PetPost : Post
{
    public Pet Pet { get; set; }
    public PetPost(string id, string title, string desc, string author, string? imageUrl, string? videoUrl, int likeCount,
        PostStatus status, DateTime created, Pet pet) : base(id, title, desc, author, imageUrl, videoUrl, likeCount, status, created)
    {
        Pet = pet;

    }
}

