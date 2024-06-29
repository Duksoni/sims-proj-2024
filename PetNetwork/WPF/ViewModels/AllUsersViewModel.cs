using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PetNetwork.WPF.ViewModels;

public class AllUsersViewModel : BaseViewModel
{
    private readonly UserService _userService;

    public ObservableCollection<PersonInfoViewModel> AllUsers { get; }

    public ICollectionView Users { get; }

    private PersonInfoViewModel? _selectedUser;
    public PersonInfoViewModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand AcceptUserCommand =>
        new(_ => ChangeUserStatus(AccountStatus.Active), _ => IsValidAccountApproval());

    public RelayCommand RejectUserCommand => 
        new(_ => ChangeUserStatus(AccountStatus.Rejected), _ => IsValidAccountApproval());

    public RelayCommand BlockUserCommand =>
        new(_ => ChangeUserStatus(AccountStatus.Blocked), _ => IsValidBlock());

    public RelayCommand PromoteUserCommand =>
        new(_ => PromoteUser(), _ => IsValidPromote());

    private void PromoteUser()
    {
        _userService.PromoteUser(SelectedUser!.Email);
        SetCollection();
    }

    public AllUsersViewModel()
    {        
        var accountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        _userService = new UserService(accountRepo, personRepo);
        AllUsers = new ObservableCollection<PersonInfoViewModel>();
        Users = CollectionViewSource.GetDefaultView(AllUsers);
        SetCollection();
    }

    private void SetCollection()
    {
        var userAccounts = _userService.GetAllAccounts(true);
        var usersPersonalInfo = _userService.GetAllPersonalInfo(true);
        var models =
            (from UserAccount account in userAccounts
                from Person person in usersPersonalInfo
                where account.Id == person.Id
                select new PersonInfoViewModel(account, person)).ToList();

        AllUsers.Clear();
        foreach (var model in models)
            AllUsers.Add(model);
    }

    private bool IsLoggedInSelected() => SelectedUser?.Email == UserSession.Session?.Account.Id;

    private bool IsValidAccountApproval() => !IsLoggedInSelected() &&
                                             SelectedUser is {Role: AccountRole.RegularUser, Status: AccountStatus.PendingApproval};

    private bool IsValidBlock() => !IsLoggedInSelected() &&
                                   SelectedUser is {Role: AccountRole.RegularUser, Status: AccountStatus.Active} or
                                       {Role: AccountRole.Volunteer, Status: AccountStatus.Active};

    private bool IsValidPromote() => !IsLoggedInSelected() &&
                                     SelectedUser is {Role: AccountRole.RegularUser, Status: AccountStatus.Active};

    private void ChangeUserStatus(AccountStatus newStatus)
    {
        _userService.UpdateAccountStatus(SelectedUser!.Email, newStatus);
        SetCollection();
    }
}