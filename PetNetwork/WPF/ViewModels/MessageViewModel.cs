using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace PetNetwork.WPF.ViewModels;

public class MessageViewModel : BaseViewModel
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

    private string _sender;
    public string Sender
    {
        get => _sender;
        set
        {
            if (_sender == value) return;
            _sender = value;
            OnPropertyChanged();
        }
    }

    private string? _recipient;
    public string? Recipient
    {
        get => _recipient;
        set
        {
            if (_recipient == value) return;
            _recipient = value;
            OnPropertyChanged();
        }
    }

    private string? _groupName;
    public string? GroupName
    {
        get => _groupName;
        set
        {
            if (_groupName == value) return;
            _groupName = value;
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

    private string _body;
    public string Body
    {
        get => _body;
        set
        {
            if (_body == value) return;
            _body = value;
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

    private ImageSource? _imageSource;
    public ImageSource? ImageSource
    {
        get => _imageSource;
        set
        {
            if (_imageSource == value) return;   
            _imageSource = value;
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    private ImageSource? LoadImage()
    {
        // Convert imageUrl to ImageSource
        if (!string.IsNullOrEmpty(ImageUrl))
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImageUrl);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            ImageSource = bitmap;
        }
        else
        {
            ImageSource = null; // Or set a default image source
        }
        return ImageSource;
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

    private MediaElement? _mediaElement;
    public MediaElement? MediaElement
    {
        get => _mediaElement;
        set
        {
            if (_mediaElement == value) return;
            _mediaElement = value;
            OnPropertyChanged(nameof(MediaElement));
        }
    }

    private MediaElement? LoadVideo()
    {
        // Convert videoUrl to MediaElement
        if (!string.IsNullOrEmpty(VideoUrl))
        {
            MediaElement = new MediaElement();
            MediaElement.Source = new Uri(VideoUrl);
            MediaElement.LoadedBehavior = MediaState.Manual; // Ensure the video doesn't auto-play
        }
        else
        {
            MediaElement = null; // Or set a default media element
        }
        return MediaElement;
    }

    private DateTime _sendTime;
    public DateTime SendTime
    {
        get => _sendTime;
        set
        {
            if (_sendTime == value) return;
            _sendTime = value;
            OnPropertyChanged();
        }
    }

    private MessageStatus _status;
    public MessageStatus Status
    {
        get => _status;
        set
        {
            if (_status == value) return;
            _status = value;
            OnPropertyChanged();
        }
    }

    private readonly RecipientValidation _validation = new();

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Title" => Title != string.Empty ? string.Empty : "Title can't be empty",
                "Body" => Body != string.Empty ? string.Empty : "Body can't be empty",
                "Sender" => Sender != string.Empty ? string.Empty : "Sender can't be empty",
                "Recipient" => _validation.ValidateRecipient(Recipient, GroupName),
                "SendTime" => SendTime < DateTime.Now && SendTime != DateTime.MinValue
                    ? string.Empty
                    : "Date not configured correctly",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Title", "Body", "Sender", "Recipient", "GroupName", "SendTime" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);


    public MessageViewModel()
    {
        _id = IdGenerator.Generate();
        _title = string.Empty;
        _body = string.Empty;
        _sender = string.Empty;
        _recipient = string.Empty;
        _groupName = string.Empty;
        _imageUrl = string.Empty;
        _videoUrl = string.Empty;
        _sendTime = DateTime.Now;
    }

    public MessageViewModel(Message message)
    {
        _id = message.Id;
        _title = message.Title;
        _body = message.Body;
        _sender = message.Sender;
        _recipient = message.Recipient;
        _groupName = message.GroupName;
        _imageUrl = message.ImageUrl;
        _imageSource = LoadImage();
        _videoUrl = message.VideoUrl;
        _mediaElement = LoadVideo();
        _sendTime = message.SendTime;
        _status = message.Status;

    }

    public Message ToMessage() => new Message(Id, Sender, Recipient, GroupName, Title, Body, ImageUrl, VideoUrl, SendTime, Status);
}
