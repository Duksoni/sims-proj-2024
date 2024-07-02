using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.Domain.Enums;
using PetNetwork.Application.Utility;

namespace PetNetwork.Application.UseCases;

public class MessageService
{
    private readonly IRepository<Message> _messageRepository;

    private readonly MessageGroupService _messageGroupService;

    public MessageService(IRepository<Message> messageRepository)
    {
        _messageRepository = messageRepository;
        var messageGroupRepo = Injector.CreateInstance<IRepository<MessageGroup>>();
        _messageGroupService = new MessageGroupService(messageGroupRepo);
    }

    public void AddMessage(Message message)
    {
        _messageRepository.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        message.Status = MessageStatus.Deleted;
        _messageRepository.Update(message);
    }

    public void UpdateMessage(Message newMessage)
    {
        if (newMessage.Status == MessageStatus.Deleted) return;
        Message? message = _messageRepository.Get(newMessage.Id);
        if (message == null) return;
        _messageRepository.Update(newMessage);
    }

    public Message? GetMessage(string id)
    {
        return _messageRepository.Get(id);
    }

    public IList<Message> GetMessagesForChat(string sender, string recipient)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in _messageRepository.GetAll())
        {
            if (message.Status == MessageStatus.Deleted) continue;

            if (!string.IsNullOrEmpty(message.Recipient))
            {
                if ((message.Sender == sender && message.Recipient == recipient) || (message.Sender == recipient && message.Recipient == sender))
                    messages.Add(message);
            }
            else if (!string.IsNullOrEmpty(message.GroupName) && message.GroupName == recipient)
            {
                if (_messageGroupService.IsMember(message.GroupName, sender))
                    messages.Add(message);
            }
        }
        return messages;
    }

    public void ReadMessage(Message message)
    {
        message.Status = MessageStatus.Read;
        _messageRepository.Update(message);
    }

    public void ReadAllChatMessages(string sender, string recipient)
    {
        var messages = GetMessagesForChat(sender, recipient);
        foreach (var message in messages)
        {
            ReadMessage(message);
        }
    }

    public bool IsChatRead(string sender, string recipient)
    {
        var messages = GetMessagesForChat(sender, recipient);
        foreach (var message in messages)
        {
            if (message.Status == MessageStatus.Unread) return false;
        }
        return true;
    }

    public IList<string> GetAllChats(string email)
    {
        return _messageRepository.GetAll()
            .Where(m => m.Sender == email || m.Recipient == email)
            .SelectMany(m => new List<string?> { m.Sender, m.Recipient })
            .Where(e => e != null && e != string.Empty && e != email) // Exclude the parameter email and null values
            .Distinct()
            .Cast<string>()
            .ToList();
    }

    public IList<string> GetAllGroupChats(string email)
    {
        return _messageRepository.GetAll()
            .Where(m => m.Sender == email || (m.GroupName != null && _messageGroupService.IsMember(m.GroupName, email)))
            .SelectMany(m => new List<string?> { m.GroupName })
            .Where(e => e! != null && e != string.Empty)
            .Distinct()
            .Cast<string>()
            .ToList();
    }

    public IList<Message> SearchMessages(string sender, string reciever, string pattern)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in GetMessagesForChat(sender, reciever))
        {
            if (message.Title.ToLower().Contains(pattern.ToLower())) messages.Add(message);
        }

        return messages;
    }
}
