namespace PetNetwork.Domain.Models;
public class PostLike
{
    public string Id { get; set; }
    public string Author { get; set; }
    public string PostId { get; set; }

    public PostLike(string id, string author, string postId)
    {
        Id = id;
        Author = author;
        PostId = postId;
    }
}

