using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PostViewModel : BaseViewModel
{
    private string _id;

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

    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    private string _description;

    public string Description
    {
        get => _description;
        set
        {
            if (_description == value) return;
            _description = value;
            OnPropertyChanged();
        }
    }

    private string _author;

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

    private string? _imageUrl;

    public string? ImageUrl
    {
        get => _imageUrl;
        set
        {
            if (_imageUrl == value) return;
            _imageUrl = value;
            OnPropertyChanged();
        }
    }

    private string? _videoUrl;

    public string? VideoUrl
    {
        get => _videoUrl;
        set
        {
            if (_videoUrl == value) return;
            _videoUrl = value;
            OnPropertyChanged();
        }
    }

    private int _likeCount;

    public int LikeCount
    {
        get => _likeCount;
        set
        {
            if (_likeCount == value) return;
            _likeCount = value;
            OnPropertyChanged();
        }
    }

    private PostStatus _status;

    public PostStatus Status
    {
        get => _status;
        set
        {
            if (_status == value) return;
            _status = value;
            OnPropertyChanged();
        }
    }

    private DateTime _createdAt;

    public DateTime CreatedAt
    {
        get => _createdAt;
        set
        {
            if (_createdAt == value) return;
            _createdAt = value;
            OnPropertyChanged();
        }
    }

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Title" => Title != string.Empty ? string.Empty : "Title can't be empty",
                "Description" => Description != string.Empty ? string.Empty : "Description can't be empty",
                "Author" => Author != string.Empty ? string.Empty : "Author can't be empty",
                "LikeCount" => LikeCount >= 0 ? string.Empty : "Like count can't be less than zero",
                "CreatedAt" => CreatedAt > DateTime.Now || CreatedAt == DateTime.MinValue
                    ? string.Empty
                    : "Date not configured correctly",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Title", "Description", "Author", "LikeCount", "CreatedAt" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);


    public PostViewModel()
    {
        _id = IdGenerator.Generate();
        _title = string.Empty;
        _description = string.Empty;
        _author = string.Empty;
        _imageUrl = string.Empty;
        _videoUrl = string.Empty;
        _likeCount = 0;
        _createdAt = DateTime.MinValue;
    }

    public Post ToPost() => new Post(Id, Title, Description, Author, ImageUrl, VideoUrl, LikeCount, Status, CreatedAt);
}