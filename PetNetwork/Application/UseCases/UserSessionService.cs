using System.Windows.Data;
using PetNetwork.Application.Utility;
using PetNetwork.Application.Utility.Constants;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class UserSessionService
{
    private readonly UserService _userService;

    public UserSessionService(UserService userService)
    {
        _userService = userService;
    }

    public bool Login(string email, string password, out KeyValuePair<UserAccount, Person> accountInfo)
    {
        accountInfo = default;
        if (email == GlobalConstants.AdminEmail &&
            PasswordHasher.Verify(password, GlobalConstants.AdminHashedPassword))
        {
            var admin = new UserAccount(GlobalConstants.AdminHashedPassword, GlobalConstants.AdminHashedPassword, AccountRole.Admin, AccountStatus.Active);
            accountInfo = new KeyValuePair<UserAccount, Person>(admin, new Person());
            return true;
        }

        var account = _userService.GetAccount(email);
        if (account == null || !PasswordHasher.Verify(password, account.Password))
            return false;

        var personalInfo = _userService.GetPersonalInfo(email) 
                           ?? throw new ValueUnavailableException($"No personal info found for account: {email} ");
        accountInfo = new KeyValuePair<UserAccount, Person>(account, personalInfo);
        return true;
    }

}