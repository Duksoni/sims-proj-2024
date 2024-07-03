using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Message : ISerializable
{
    public string Id { get; set; }
    public string Sender { get; set; } // user account email
    public string? Recipient { get; set; } // user account email
    public string? GroupName { get; set; } // group name
    public string Title { get; set; }
    public string Body { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }

    [JsonConverter(typeof(StringDateTimeConverter))]
    public DateTime SendTime { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public MessageStatus Status { get; set; }
    public bool Deleted
    {
        get => Status == MessageStatus.Deleted;
        set
        {
            if (Status == MessageStatus.Deleted || !value) return;
            Status = MessageStatus.Deleted;
        }
    }

    public Message(string id, string sender, string? recipient, string? groupName, string title, string body,
        string? imageUrl, string? videoUrl, DateTime sendTime, MessageStatus status)
    {
        Id = id;
        Sender = sender;
        Recipient = recipient;
        GroupName = groupName;
        Title = title;
        Body = body;
        ImageUrl = imageUrl;
        VideoUrl = videoUrl;
        SendTime = sendTime;
        Status = status;
        Deleted = false;
    }
}
