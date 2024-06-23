using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Post : ISerializable
{
    public string Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; } // user account email
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public int LikeCount { get; set; }
    public PostStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    //public IList<Comment> Comments { get; set; } // if NoSql can be used
    //public IList<string> CommentIds { get; set; } // if Sql needs to be used

    public Post(string id, string title, string desc, string author, string imageUrl, string videoUrl, int likeCount,
        PostStatus status, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = desc;
        Author = author;
        ImageUrl = imageUrl;
        VideoUrl = videoUrl;
        LikeCount = likeCount;
        Status = status;
        CreatedAt = createdAt;
    }
}

