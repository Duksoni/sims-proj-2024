using PetNetwork.Application.Utility;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PostLikeViewModel : BaseViewModel
{
    private string _id;
    private string _author;
    private Post _post;

    public string Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (_author == value) return;
            _author = value;
            OnPropertyChanged();
        }
    }

    public Post Post
    {
        get => _post;
        set
        {
            if (_post == value) return;
            _post = value;
            OnPropertyChanged();
        }
    }

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Author" => Author != string.Empty ? string.Empty : "Author can't be empty",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Author", "PostId" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);


    public PostLikeViewModel(string author, Post post)
    {
        _id = IdGenerator.Generate();
        _author = author;
        _post = post;
    }

    public PostLike ToPostLike() => new PostLike(Id, Author, Post);
}