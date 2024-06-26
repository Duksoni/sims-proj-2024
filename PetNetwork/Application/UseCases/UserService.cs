using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class UserService
{

    private readonly IRepository<UserAccount> _userAccountRepository;
    private readonly IRepository<Person> _personRepository;

    public UserService(IRepository<UserAccount> userAccountRepository, IRepository<Person> personRepository)
    {
        _userAccountRepository = userAccountRepository;
        _personRepository = personRepository;
    }

    public void OpenAccount(UserAccount newAccount, Person userInfo)
    {
        _userAccountRepository.Add(newAccount);
        _personRepository.Add(userInfo);
    }

    public void CloseAccount(string email)
    {
        _userAccountRepository.Remove(email);
        _personRepository.Remove(email);
    }

    public void UpdateAccountStatus(string email, AccountStatus newStatus)
    {
        if (newStatus == AccountStatus.Deleted)
        {
            CloseAccount(email);
            return;
        }
        var matchedAccount = _userAccountRepository.Get(email);
        if (matchedAccount == null) return;
        matchedAccount.Status = newStatus;
        _userAccountRepository.Update(matchedAccount);
    }

    public UserAccount? GetAccount(string email) => _userAccountRepository.Get(email);

    public Person? GetPersonalInfo(string email) => _personRepository.Get(email);

    public IList<Person> GetUserPersonalInfoRange(IList<string> accountEmails) => 
        _personRepository.GetAll().Where(person => accountEmails.Contains(person.Id)).ToList();

    public IList<UserAccount> FindUserAccounts(AccountRole role) => 
        _userAccountRepository.Find(account => account.Role == role && account.Status != AccountStatus.Deleted);

    public IList<Person> FindUsersPersonalInfo(AccountRole role)
    {
        var accountEmails = FindUserAccounts(role).Select(account => account.Id).ToList();
        return GetUserPersonalInfoRange(accountEmails);
    }
}