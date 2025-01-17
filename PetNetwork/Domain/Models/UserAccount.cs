﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class UserAccount : ISerializable
{
    [JsonProperty("Email")]
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
        get => Status == AccountStatus.Blocked;
        set
        {
            if (Status == AccountStatus.Blocked || !value) return;
            Status = AccountStatus.Blocked;
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
    public UserAccount(string email, string password, AccountRole role, AccountStatus status)
    {
        Id = email;
        _password = password;
        Role = role;
        Status = status;
    }
}