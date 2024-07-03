using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Application.Utility;
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

    [JsonConverter(typeof(StringEnumConverter))]
    public PostStatus Status { get; set; }
    [JsonConverter(typeof(StringDateTimeConverter))]
    public DateTime CreatedAt { get; set; }
    public bool Deleted
    {
        get => Status == PostStatus.Deleted;
        set
        {
            if (Status == PostStatus.Deleted || !value) return;
            Status = PostStatus.Deleted;
        }
    }

    public Post(string id, string title, string desc, string author, string? imageUrl, string? videoUrl, int likeCount,
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
        Deleted = false;
    }
}

