using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.Domain.Enums;

namespace PetNetwork.Application.UseCases;

public class MessageService
{
    private readonly IRepository<Message> _messageRepository;

    private readonly MessageGroupService _messageGroupService;

    public MessageService(IRepository<Message> messageRepository, MessageGroupService messageGroupService)
    {
        _messageRepository = messageRepository;
        _messageGroupService = messageGroupService;
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

    public IList<Message> GetAllMessages(string recipient)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in _messageRepository.GetAll())
        {
            if (message.Recipient != null && message.Recipient == recipient) messages.Add(message);

            else if (message.GroupName != null && _messageGroupService.IsMember(message.GroupName, recipient))
                messages.Add(message);
        }
        return messages;
    }

    public IList<Message> GetSentMessages(string sender)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in _messageRepository.GetAll())
        {
            if (message.Sender == sender) messages.Add(message);
        }
        return messages;
    }

    public IList<Message> GetUnreadMessages(string recipient)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in GetAllMessages(recipient))
        {
            if (message.Status == MessageStatus.Unread) messages.Add(message);
        }
        return messages;
    }

    public void ReadMessage(string id)
    {
        Message? message = _messageRepository.Get(id);
        if (message == null) return;
        message.Status = MessageStatus.Read;
        _messageRepository.Update(message);
    }

    public IList<Message> SearchMessages(string pattern, string recipient)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in GetAllMessages(recipient))
        {
            if (message.Title.ToLower().Contains(pattern.ToLower())) messages.Add(message);
        }
        return messages;
    }
}
