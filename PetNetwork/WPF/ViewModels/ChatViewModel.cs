using PetNetwork.Domain.Enums;

namespace PetNetwork.WPF.ViewModels;

public class ChatViewModel : BaseViewModel
{
    private string _chat;
    public string Chat
    {
        get => _chat;
        set
        {
            if (_chat == value) return;
            _chat = value;
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

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Chat" => Chat != string.Empty ? string.Empty : "Chat can't be empty",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Chat" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);


    public ChatViewModel(string chat, bool read)
    {
        _chat = chat;
        _status = read ? MessageStatus.Read : MessageStatus.Unread;
    }
}
