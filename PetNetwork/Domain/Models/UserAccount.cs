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

    private AccountRole _role;
    public AccountRole Role
    {
        get => _role;
        set
        {
            if (value == AccountRole.Admin) throw new ArgumentException("No new admin can be added");
            _role = value;
        }
    }

    public AccountStatus Status { get; private set; }

    public UserAccount(string email, string password)
    {
        Id = email;
        Password = password;
        Role = AccountRole.RegularUser;
        Status = AccountStatus.PendingApproval;
    }

    public UserAccount(string email, string password, AccountRole role)
    {
        Id = email;
        Password = password;
        Role = role;
        Status = AccountStatus.PendingApproval;
    }

    public UserAccount(string id, string password, AccountRole role, AccountStatus status)
    {
        Id = id;
        Password = password;
        Role = role;
        Status = status;
    }
}