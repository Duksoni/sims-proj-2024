using PetNetwork.Application.Utility;
using PetNetwork.Application.Utility.Constants;
using PetNetwork.Domain.Enums;
using System.Collections.ObjectModel;
using PetNetwork.Application.UseCases;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class UserRegistrationViewModel
{
    private readonly UserService _userService;

    public ObservableCollection<Gender> Genders { get; } =
        new(Enum.GetValues(typeof(Gender))
            .Cast<Gender>());

    public PersonViewModel Person { get; }

    public UserAccountViewModel Account { get; }

    public bool RegistrationSuccess { get; private set; }

    public RelayCommand SubmitRegistrationCommand => new(_ => TrySubmitting());

    public UserRegistrationViewModel(AccountRole role)
    {
        Person = new PersonViewModel();
        Account = new UserAccountViewModel(role);
        var accountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        _userService = new UserService(accountRepo, personRepo);
    }

    public void TrySubmitting()
    {
        if (!Person.IsValid || !Account.IsValid)
        {
            MessageDisplay.ErrorMessage(UserMessages.FailedRegistration, "Registration fail");
            return;
        }

        try
        {
            var account = Account.ToUserAccount();
            var person = Person.ToPerson(account.Id);
            _userService.OpenAccount(account, person);
            RegistrationSuccess = true;
        }
        catch (Exception ex)
        {
            MessageDisplay.ErrorMessage(ex.Message, "Registration fail");
        }
    }

}