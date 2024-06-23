using PetNetwork.Application.Utility;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PostLikeViewModel : BaseViewModel
{
    private string _id;
    private string _author;
    private string _postId;

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

    public string PostId
    {
        get => _postId;
        set
        {
            if (_postId == value) return;
            _postId = value;
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
                "PostId" => PostId != string.Empty ? string.Empty : "PostId can't be empty",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Author", "PostId" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);

    public PostLikeViewModel()
    {
        _id = IdGenerator.Generate();
        _author = string.Empty;
        _postId = string.Empty;
    }

    public PostLike ToPostLike() => new PostLike(Id, Author, PostId);
}