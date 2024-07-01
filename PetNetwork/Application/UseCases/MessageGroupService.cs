using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class MessageGroupService
{
    private readonly IRepository<MessageGroup> _messageGroupsRepository;

    public MessageGroupService(IRepository<MessageGroup> messageGroupsRepository)
    {
        _messageGroupsRepository = messageGroupsRepository;
    }

    public void AddGroup(MessageGroup messageGroup)
    {
        _messageGroupsRepository.Add(messageGroup);
    }

    public void DeleteGroup(MessageGroup messageGroup)
    {
        messageGroup.Deleted = true;
        _messageGroupsRepository.Update(messageGroup);
    }

    public MessageGroup? GetGroup(string id)
    {
        return _messageGroupsRepository.Get(id);
    }

    public IList<MessageGroup> GetAll()
    {
        return _messageGroupsRepository.GetAll();
    }

    public IList<MessageGroup> GetJoinedGroups(string email)
    {
        IList<MessageGroup> groups = new List<MessageGroup>();
        foreach (var group in _messageGroupsRepository.GetAll())
        {
            if (IsMember(group.Id, email)) 
                groups.Add(group);
        }
        return groups;
    }

    public void JoinGroup(MessageGroup messageGroup, string email)
    {
        messageGroup.Members.Add(email);
        _messageGroupsRepository.Update(messageGroup);
    }

    public void LeaveGroup(MessageGroup messageGroup, string email)
    {
        messageGroup.Members.Remove(email);
        _messageGroupsRepository.Update(messageGroup);
    }

    public bool IsMember(string groupName,  string email)
    {
        var group = _messageGroupsRepository.Get(groupName);
        if (group == null) return false;

        foreach (var member in group.Members)
        {
            if (member ==  email) return true;
        }
        return false;
    }
}
