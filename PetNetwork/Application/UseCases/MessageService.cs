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
            else if (!string.IsNullOrEmpty(message.GroupName))
            {
                if (_messageGroupService.IsMember(message.GroupName, sender) && _messageGroupService.IsMember(message.GroupName, recipient))
                    messages.Add(message);
            }
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

    public IList<string> GetAllChats(string email)
    {
        return _messageRepository.GetAll()
            .Where(m => m.Sender == email || m.Recipient == email || (m.GroupName != null && _messageGroupService.IsMember(m.GroupName, email)))
            .SelectMany(m => new List<string?> { m.Sender, m.Recipient, m.GroupName })
            .Where(e => e != null && e != string.Empty && e != email) // Exclude the parameter email and null values
            .Distinct()
            .Cast<string>()
            .ToList();
    }
}
