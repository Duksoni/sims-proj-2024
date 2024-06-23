namespace PetNetwork.Domain.Models;

public class Comment
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; } // user account email

    public Comment(string id, string text, string author)
    {
        Id = id;
        Text = text;
        Author = author;
    }
}

