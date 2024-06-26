using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels;

public class UserAccountViewModel : BaseViewModel, IDataErrorInfo
{
    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set 
        {
            if (_email == value) return;
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set 
        {
            if (_password == value) return;
            _password = value;
            OnPropertyChanged();
        }
    }

    private readonly AccountRole _role;

    public UserAccountViewModel(AccountRole role = AccountRole.RegularUser)
    {
        if (role == AccountRole.Admin)
            throw new ArgumentException("Can't add new admin");
        _role = role;
    }

    public UserAccount ToUserAccount() =>
        _role == AccountRole.RegularUser 
            ? new UserAccount(_email, _password) 
            : new UserAccount(_email, _password, AccountRole.Volunteer, AccountStatus.Active);

    private readonly UserAccountValidation _validation = new();

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Email" => _validation.ValidateEmail(Email),
                "Password" => _validation.ValidatePassword(Password),
                _ => string.Empty
            };
        }
    }

    public string Error => string.Empty;

    public bool IsValid => this[nameof(Email)] == string.Empty && this[nameof(Password)] == string.Empty;
}