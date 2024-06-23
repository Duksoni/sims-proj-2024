using PetNetwork.Application.Utility;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;
public class CommentViewModel : BaseViewModel
{
    private string _id;
    private string _text;
    private string _author;
    private DateTime _createdAt;

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

    public string Text
    {
        get => _text;
        set
        {
            if (_text == value) return;
            _text = value;
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
                "Text" => Text != string.Empty ? string.Empty : "Text can't be empty",
                "Author" => Author != string.Empty ? string.Empty : "Author can't be empty",
                "CreatedAt" => CreatedAt > DateTime.Now || CreatedAt == DateTime.MinValue
                    ? string.Empty
                    : "Date not configured correctly",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Text", "Author", "CreatedAt" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);

    public CommentViewModel()
    {
        _id = IdGenerator.Generate();
        _text = string.Empty;
        _author = string.Empty;
        _createdAt = DateTime.MinValue;
    }

    public Comment ToComment() => new Comment(Id, Text, Author, CreatedAt);
}
