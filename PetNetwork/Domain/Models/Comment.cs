namespace PetNetwork.Domain.Models;

public class Comment
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; } // user account email
    public Post Post { get; set; }
    public DateTime CreatedAt { get; set; }

    public Comment(string id, string text, string author, Post post, DateTime createdAt)
    {
        Id = id;
        Text = text;
        Author = author;
        Post = post;
        CreatedAt = createdAt;
    }
}

