using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.Domain.Enums;

namespace PetNetwork.Application.UseCases;

public class MessageService
{
    private readonly IRepository<Message> _messageRepository;

    public MessageService(IRepository<Message> messageRepository)
    {
        _messageRepository = messageRepository;
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

    public IList<Message> GetAllMessages()
    {
        return _messageRepository.GetAll();
    }

    public IList<Message> GetUnreadMessages()
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in _messageRepository.GetAll())
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

    public IList<Message> SearchMessages(string pattern)
    {
        IList<Message> messages = new List<Message>();
        foreach (var message in _messageRepository.GetAll())
        {
            if (message.Title.ToLower().Contains(pattern.ToLower())) messages.Add(message);
        }
        return messages;
    }
}
