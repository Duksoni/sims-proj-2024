using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Application.Utility.Constants;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly UserSessionService _userSessionService;

    public RelayCommand TryLoginCommand => new(_ => TryUserLogin());

    private string _email = string.Empty;
    public string Email
    {
        private get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        private get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    private string _loginResultMessage = string.Empty;
    public string LoginResultMessage
    {
        get => _loginResultMessage;
        set
        {
            _loginResultMessage = value;
            OnPropertyChanged();
        }
    }


    public LoginViewModel()
    {
        var accountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        var userService = new UserService(accountRepo, personRepo);
        _userSessionService = new UserSessionService(userService);
    }

    private void TryUserLogin()
    {
        if (!AreValidInputs()) return;

        if (!_userSessionService.Login(Email, Password, out var accountInfo))
        {
            LoginResultMessage = UserMessages.UserNotFound;
            return;
        }

        switch (accountInfo.Key)
        {
            case { Status: AccountStatus.Blocked }:
                LoginResultMessage = UserMessages.AccountDisabled;
                break;
            case { Status: AccountStatus.PendingApproval }:
                LoginResultMessage = UserMessages.PendingApproval;
                break;
            case { Status: AccountStatus.Rejected }:
                LoginResultMessage = UserMessages.Rejected;
                break;
            default:
                UserSession.Start(accountInfo.Key, accountInfo.Value);
                break;
        }
    }

    private bool AreValidInputs()
    {
        if (Email != string.Empty && Password != string.Empty) return true;
        LoginResultMessage = UserMessages.InvalidLogin;
        return false;
    }
}