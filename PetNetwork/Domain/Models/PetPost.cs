using PetNetwork.Domain.Enums;

namespace PetNetwork.Domain.Models;

public class PetPost : Post
{
    //public string PetId { get; set; }
    public PetPost(string id, string title, string desc, string author, string imageUrl, string videoUrl, int likeCount,
        PostStatus status, DateTime created) : base(id, title, desc, author, imageUrl, videoUrl, likeCount, status, created)
    {
        // needs petId here
        //PetId = petId;
    }
}

