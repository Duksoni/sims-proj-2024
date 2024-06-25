using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class UserAccount : ISerializable
{
    public string Id { get; }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set
        {
            if (value == string.Empty) throw new ArgumentException("Password can't be empty");
            _password = PasswordHasher.Hash(value);
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public AccountRole Role { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public AccountStatus Status { get; set; }

    [JsonIgnore]
    public bool Deleted
    {
        get => Status == AccountStatus.Deleted;
        set
        {
            if (Status == AccountStatus.Deleted || !value) return;
            Status = AccountStatus.Deleted;
        }
    }

    public UserAccount(string email, string password)
    {
        Id = email;
        Password = password;
        Role = AccountRole.RegularUser;
        Status = AccountStatus.PendingApproval;
    }

    public UserAccount(string email, string password, AccountRole role)
    {
        if (role == AccountRole.Admin) throw new ArgumentException("No new admin can be added");
        Id = email;
        Password = password;
        Role = role;
        Status = AccountStatus.Active;
    }

    [JsonConstructor]
    public UserAccount(string id, string password, AccountRole role, AccountStatus status)
    {
        Id = id;
        _password = password;
        Role = role;
        Status = status;
    }
}