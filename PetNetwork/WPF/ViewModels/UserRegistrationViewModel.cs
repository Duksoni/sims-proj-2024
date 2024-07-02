using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels;

public class UserRegistrationViewModel : BaseViewModel
{
    private readonly UserService _userService;

    public ObservableCollection<Gender> Genders { get; } =
        new(Enum.GetValues(typeof(Gender))
            .Cast<Gender>());

    public PersonViewModel Person { get; }

    public UserAccountViewModel Account { get; }

    private bool _registrationSuccess;
    public bool RegistrationSuccess
    {
        get => _registrationSuccess;
        private set
        {
            _registrationSuccess = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand SubmitRegistrationCommand => new(_ => TrySubmitting(), _ => Person.IsValid && Account.IsValid);

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